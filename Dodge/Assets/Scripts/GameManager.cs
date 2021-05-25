using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 라이브러리
using UnityEngine.SceneManagement; // 씬 관리 관련 라이브러리
//게임시작 / 종료관리, /스코어 관리(시간) 이런걸 하는 것이 게임매너지의 역할!
public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;// 게임오버시 확성화할 텍스트 게임 오브젝트
    public Text timeText;
    public Text recordText;


    public GameObject level; // 불렛등 레벨 수정할 변수
    public GameObject bulletSpawnerPrefab;
    public GameObject itemPrefab;
    int prevItemCheck;

    private Vector3[] bulletSpawners = new Vector3[4];
    int spawnCounter = 0;

    private float surviveTime; // 생존시간
    private bool isGameover; // 게임오버 상태
    
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameover = false;

        bulletSpawners[0].x = -8f;
        bulletSpawners[0].y = 1f;
        bulletSpawners[0].z = 8f;

        bulletSpawners[1].x = 8f;
        bulletSpawners[1].y = 1f;
        bulletSpawners[1].z = 8f;

        bulletSpawners[2].x = 8f;
        bulletSpawners[2].y = 1f;
        bulletSpawners[2].z = -8f;

        bulletSpawners[3].x = -8f;
        bulletSpawners[3].y = 1f;
        bulletSpawners[3].z = -8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameover)
        {
            //생존 시간 갱신
            surviveTime += Time.deltaTime;
            //갱신한 생존시간을 timeText 텍스트 컴포넌트를 이용해 표시
            timeText.text = "Time: " + (int)surviveTime;

            if(surviveTime % 5f <= 0.01f && prevItemCheck == 4)
            {
                Vector3 randpos = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));

                GameObject item = Instantiate(itemPrefab, randpos, Quaternion.identity);
                item.transform.parent = level.transform;
                item.transform.localPosition = randpos;
            }
            prevItemCheck = (int)(surviveTime % 5f);



            if (surviveTime < 5f && spawnCounter == 0)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            if (surviveTime >= 5f && surviveTime < 10f && spawnCounter == 1)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            if (surviveTime >= 10f && surviveTime < 15f && spawnCounter == 2)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
            if (surviveTime >= 15f && surviveTime < 20f && spawnCounter == 3)
            {
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, bulletSpawners[spawnCounter], Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = bulletSpawners[spawnCounter];
                level.GetComponent<Rotator>().rotationSpeed += 15f;
                spawnCounter++;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        recordText.text = "Best Time: " + (int)bestTime;
    }
}
