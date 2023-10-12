using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 생성할 탄알의 원본 프리팹
    public float spawnRateMin = 0.5f;   // 최소 생성 주기
    public float spawnRateMax = 3f; // 최대 생성 주기

    private Transform target;   // 발사할 대상
    private float spawnRate;    // 생성 주기
    private float timeAfterSpawn;   // 최근 생성 시점에서 지난 시간
    void Start()
    {
        // 최근 생성 이후의 누적 시간을 0으로 초기화
        timeAfterSpawn = 0f;
        // 탄알 생성 간격을 spawnRateMin 과 spawnRateMax 사이에서 랜덤 지정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        // PlayerController 컴포넌트를 가진 게임 오브젝트를 찾아 조준 대상으로 설정
        target = FindObjectOfType<PlayerController>().transform;        // ??? 질문 : 플레이어가 두명이상이면 이 함수는 어떤식으로 동작하지?
                                                                        // FindObjectOfType() 메서드는 꺾쇠 <>에 어떤 타입을 명시하면 씬에 있는 모든 오브젝트를 검생해서 해당 타입의 오브젝트를 가져옴
                                                                        // 아마 배열로 잡아야겠지?  좀 있다 실험
    }

    void Update()
    {
        // timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;

        // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            // 누적된 시간을 리셋
            timeAfterSpawn = 0f;

            // bulletPrefab의 복제본을
            // transform.position 위치와 transform.rotation 회전으로 생성
            GameObject bullet
                = Instantiate(bulletPrefab, transform.position, transform.rotation);
            // 생성된 bullet 게임 오브젝트의 정면 방향이 target을 향하도록 회전
            bullet.transform.LookAt(target);

            // 다음번 생성 간격을 spawnRateMin, spawnRateMax 사이에서 랜덤 지정
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
              
        }
    }   
}
