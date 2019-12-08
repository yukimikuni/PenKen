using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
// using UnityEditor;

public class GameController : MonoBehaviour {

	public GameObject[] notes;
    public GameObject[] comboEffects;
    public float[] noteX;
	private float[] _timing;
	private int[] _lineNum;
    private int[] _type;
    private int[] _comboType;
    private int[] _comboMax;
    private GameObject[] _noteInstance;

    public string filePass;
	private int _notesCount = 0;

	private AudioSource _audioSource;
	private float _startTime = 0;

	public float timeOffset = -1;

	private bool _isPlaying = false;
	public GameObject startButton;

	public Text scoreText;
    public int maxNotes = 1024;
    public float yPos = 10.0f;

    private Canvas _rootCanvas;
    public GameObject mTapEffect;
    public GameObject ChangeSceneObject;
    public Vector2[] buttonPos;
    public bool debugPause;

    public int currentComboType;
    public int comboCount;
    public AudioClip[] buttonSounds;
    public AudioClip startSound;
    public NoteDataStore noteDataStore;
   

    void Start(){

        //追加のDontDestroyOnLoad
        // DontDestroyOnLoad(this);

        _audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        _rootCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		_timing = new float[maxNotes];
		_lineNum = new int[maxNotes];
        _type = new int[maxNotes];
        _comboType = new int[maxNotes];
        _comboMax = new int[maxNotes];
        _noteInstance = new GameObject[maxNotes];
        if (
                scoreText != null)
        {
            scoreText.GetComponent<Text>().text = "SCORE  " + ScoreManager.score.ToString();
        }

        //LoadCSV ();
        LoadCSVFromNoteDataStore();
    }

	void Update () {
		if (_isPlaying) {
			CheckNextNotes ();
		}
			
	}

	public void StartGame(){
        gameObject.GetComponent<AudioSource>().clip = startSound;
        gameObject.GetComponent<AudioSource>().Play();

        startButton.SetActive (false);
		_startTime = Time.time;
		_audioSource.Play ();
		_isPlaying = true;
        ChangeSceneObject.GetComponent<ChangeScenes>().startChangeScene();

    }

	void CheckNextNotes(){
        if ( _notesCount > maxNotes)
        {
            return;
        }
		while (_timing [_notesCount] + timeOffset < GetMusicTime () && Math.Abs(_timing [_notesCount]) >= 0.0001f) {
			GameObject spawnedNotes = SpawnNotes (_lineNum[_notesCount], _type[_notesCount]);
            spawnedNotes.GetComponent<NotesScript>().noteCount = _notesCount;
            spawnedNotes.GetComponent<NotesScript>().lineNum = _lineNum[_notesCount];
            spawnedNotes.GetComponent<NotesScript>().comboType = _comboType[_notesCount];
            spawnedNotes.GetComponent<NotesScript>().comboMax = _comboMax[_notesCount];
            spawnedNotes.GetComponent<NotesScript>().noteType = _type[_notesCount];

            _noteInstance[_notesCount] = spawnedNotes;


            _notesCount++;
		}
	}

	GameObject SpawnNotes(int lineNum, int typeNum){
        //Debug.Log(num);
        //
        GameObject obj = Instantiate (notes[typeNum],
			Vector3.zero,
			Quaternion.identity,
            _rootCanvas.transform
            );

        obj.GetComponent<RectTransform>().localPosition = new Vector3(noteX[lineNum], yPos, 0);

        return obj;
	}

	void LoadCSV(){
		int i = 0, j;
		TextAsset csv = Resources.Load (filePass) as TextAsset;
		StringReader reader = new StringReader (csv.text);
		while (reader.Peek () > -1) {
			
			string line = reader.ReadLine ();
			string[] values = line.Split (',');
			for (j = 0; j < values.Length; j++) {
				_timing [i] = float.Parse( values [0] );
				_lineNum [i] = int.Parse( values [1] );
                _type[i] = int.Parse(values[2]);
                _comboType[i] = int.Parse(values[3]);
                _comboMax[i] = int.Parse(values[4]);
            }
			i++;
		}
	}

    void LoadCSVFromNoteDataStore()
    {
        for (int i = 0; i < noteDataStore.datas.Length; i++)
        {
            _timing[i] = noteDataStore.datas[i].timing;
            _lineNum[i] = noteDataStore.datas[i].lineNum;
            _type[i] = noteDataStore.datas[i].type;
            _comboType[i] = noteDataStore.datas[i].comboType;
            _comboMax[i] = noteDataStore.datas[i].comboMax;
        }
    }

    float GetMusicTime(){
		return Time.time - _startTime;
	}

	public void GoodTimingFunc(int num){
		Debug.Log ("Line:" + num + " good!");
		Debug.Log (GetMusicTime());
		// 追加
		EffectManager.Instance.PlayEffect(num);
	}

    public void SetNullToNoteInstance( int index)
    {
        _noteInstance[index] = null;
    }

    public void CheckNotes(int index)
    {
        // Debug.Log(index);

        //タップエフェクトを出す

        float x = Screen.width / 2 + buttonPos[index].x;
        float y = Screen.height / 2 + buttonPos[index].y;

        // Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2 - 274.0f + 65.0f / 2, -0.8f + 65.0f / 2));

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        Instantiate(mTapEffect, worldPos, Quaternion.identity, transform);

        //if (debugPause)
        //{
        //    EditorApplication.isPaused = true;
        //}

        bool hitNote = false;
        //int comboMax = 0 ;
        int currentComboMax = -1;

        for ( int i = 0; i < maxNotes; i ++)
        {
            if ( _noteInstance[i] != null)
            {
                GameObject note = _noteInstance[i];
//                Debug.Log(i);
                if (note.GetComponent<NotesScript>().isInLine)
                {
                    Debug.Log("InLine");
                    //Debug.Log(note.GetComponent<NotesScript>().lineNum);
                    if (note.GetComponent<NotesScript>().lineNum == index)
                    {
                        //Debug.Log("Hit");
                        // 点数入れる
                        //Debug.Log(score);
                        //Debug.Log(score.ToString());
                        ScoreManager.score++;
                        // scoreText.GetComponent<Text>().text = "SCORE  " + score.ToString();
                        scoreText.GetComponent<Text>().text = "SCORE  " + ScoreManager.score.ToString();

                        hitNote = true;
                        int noteComboType = _noteInstance[i].GetComponent<NotesScript>().comboType;
                        if (currentComboType != noteComboType)
                        {
                            comboCount = 0;
                        }
                        if (noteComboType == 0)
                        {
                            comboCount = 0;
                        }
                        if (noteComboType > 0) {
                            comboCount++;
                            currentComboType = noteComboType;
                            currentComboMax = _noteInstance[i].GetComponent<NotesScript>().comboMax;

                            /*if (score <= 10)
                                return Animation[0];

                            if (score <= 30)
                                return nextSceneNames[1];

                            if (score <= 50)
                                return nextSceneNames[2];

                            if (score <= 70)
                                return nextSceneNames[2];
                                */



                        }
                        else
                        {
                            comboCount = 0;
                        }

                        gameObject.GetComponent<AudioSource>().clip = buttonSounds[_noteInstance[i].GetComponent<NotesScript>().noteType];
                        gameObject.GetComponent<AudioSource>().Play();


                        // comboMax
                        Destroy(_noteInstance[i]);
                        _noteInstance[i] = null;

                        
                    }
                }
            }
        }
        
        if ( hitNote )
        {
            if (comboCount > 0)
            {
                if (comboCount == currentComboMax)
                {
                    Debug.Log("Combo!!!");

                    // アニメーションの変更
                    // combo effect
                    if (currentComboType == 1)
                    {
                        Instantiate(comboEffects[0]);
                    }
                    if (currentComboType == 2)
                    {
                        Instantiate(comboEffects[1]);
                    }
                }
            }
        }
        else
        {
            currentComboType = -1;
        }
        
    }

}
