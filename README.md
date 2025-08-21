# 용병단 키우기 – 포트폴리오 정리 (전투/파견/성장 시스템)

> Unity 2022.3.25f1 · C# · 1인 개발

---

## 1) 프로젝트 개요

* **장르/콘셉트**: 용병단을 성장시키며 파견과 전투를 통해 자원을 획득하는 싱글 플레이 게임.
* **핵심 시스템**: 전투(Battle) · 파견(Dispatch) · 성장(Training) · 데이터 주도 설계(Resources 로드)
* **플랫폼**: (작성자 입력) – PC/모바일 등
* **역할**: 기획/프로그래밍/툴링 전반

---

## 2) 사용 기술

* **엔진/언어**: Unity 2022.3.25f1, C#
* **아키텍처**: Scriptable/JSON·TXT 기반 데이터 주도 설계, Scene 모듈화, 인터페이스 기반 컴포넌트 연결
* **연동**: Resources 폴더 기반 외부 데이터 로드 → 런타임 적용(DataBase/Controller 계층)

---

## 3) 전체 구조 요약

```
[Title Scene]
   └─ 게임 시작/옵션 → [Main Scene]
        ├─ 파견(Dispatch)
        │    └─ JSON/TXT 로드 → 시간·확률·보상 계산 → 골드/XP 반영
        └─ 전투 입장(Battle)
             └─ (일반 적 / 보스) 선택 → [Battle Scene]
                  ├─ 전투 초기화(캐릭터/적/보스 세팅)
                  ├─ 전투 진행(애니 이벤트 → 데미지/종료 처리)
                  └─ 결과(골드/XP/보상)
```

---

## 4) 데이터 파이프라인(개요)

```
[Resources]
  ├─ characters_data.json     ─┐
  ├─ Enemy_data.json          ─┤  전투 초기 스탯/XP 세팅
  ├─ Boss_data.json           ─┘
  ├─ StageExperienceValue.txt ── 스테이지별 XP 테이블
  ├─ BossStageExperienceValue.txt ─ 보스 스테이지 XP 테이블
  ├─ Gold.json                ── 현재 보유 골드
  ├─ XpValue.txt              ── 레벨→필요 XP 테이블
  ├─ Training_0~3.json        ── 훈련 단계별(공/방/체) 비용·시간·성공확률·증가치
  └─ Dispatch_1~3.json        ── 파견 과제 세트(난이도별)

로드(Managers/Loaders) → DataBase/Controller 계층에 주입 → 씬별 시스템(Battle/Dispatch/Training)에서 소비
```

---

## 5) 전투 씬 구성 (Battle Scene)

### 핵심 오브젝트 & 컨트롤러

* **BattleCharctreController**: 전투 시작 시 플레이어/적/보스 오브젝트를 찾아 초기화, 현재 선택한 적/보스 인덱스에 맞춰 스프라이트와 스탯을 세팅. 보스/일반 적 모드를 분기하여 활성화 상태 관리.
* **BattleEffecController**: Attack 영역과 클릭 방지 영역 초기화, 전투 연출/입력 제한 시 사용.
* **BattleAniEvent**: 애니메이션 이벤트 훅. 공격 타이밍에 BattleContentController의 데미지 계산 함수를 호출, 공격 종료 등의 이벤트 트리거 제공.

### 전투 초기화 플로우(의사 코드)

```
OnBattleSceneEnable()
  1) 플레이어/적/보스 루트 Transform 캐시
  2) 기본 활성 상태: My 활성, Enemy/보스 비활성
  3) DataBase의 "보스 전투 여부" 플래그 확인
     - 보스전: 보스 초기화 → 스프라이트/스탯 세팅 → 현재 EnemyXP 갱신
     - 일반전: Enemy N개 루프 초기화 → 스프라이트/스탯 세팅 → 현재 EnemyXP 갱신
```

### 애니메이션 이벤트 연동

```
애니메이션 클립 타임라인 → Event: Function_AniEventAttackDamages()
  └─ BattleContentController.Function_AniEventAttackDamages() 호출
```

---

## 6) 파견(Dispatch) & 성장(Training)

### 파견 시스템

* Dispatch\_1/2/3.json의 과제 목록을 난이도(혹은 챕터) 단위로 분리 관리
* 각 과제: `id`, `name`, `time`, `gold`, `probability` → 실행 시 소요시간·성공확률·보상 계산
* DispatchXpValue.txt로 파견 보상 XP(단계별 기본값) 테이블을 참조(규칙은 로더/매니저와 함께 정의)

### 성장(Training) 시스템

* Training\_0\~3.json: 동일한 3종 스탯(공/방/체)에 대해 단계별 `stats(증가치)`, `time(소요시간)`, `gold(비용)`, `probability(성공확률)` 제공
* 성공 시 캐릭터 스탯 증가, 실패 시 재시도(연출/패널티 정책은 구현에 따름)

---

## 7) 데이터 스키마(요약)

> 실제 필드와 예시는 데이터 파일 참조(포트폴리오 제출 시 일부만 캡처/익명화 권장)

### 전투 관련

* **characters\_data.json**: 캐릭터 배열

  * 예시 필드: `id`, `name`, `level`, `currentXP`, `baseStats{attack, defense, health}`, `trainingStats{...}`
* **Enemy\_data.json / Boss\_data.json**: 적/보스 스탯 배열

  * 공통 필드: `id`, `name`, `currentXP`, `baseStats{attack, defense, health}`, `gold`
* **StageExperienceValue.txt / BossStageExperienceValue.txt**: `스테이지 인덱스  경험치`
* **XpValue.txt**: `레벨  필요경험치` 테이블

### 경제/파견/성장

* **Gold.json**: `{ currentGold: number }`
* **Dispatch\_1\~3.json**: `id`, `name`, `time`, `gold`, `probability`
* **DispatchXpValue.txt**: 파견 단계별 기본 XP 값(리스트)
* **Training\_0\~3.json**: `training[]` 안에 `id`, `name(공/방/체)`, `stats(증가치)`, `time`, `gold`, `probability`

---

## 8) 대표 흐름도

### A) 전투 입장

```
[메인] ─▶ 전투 버튼
    └─ 보스전 여부 확인
        ├─ 예: [보스 전투]
        │    └─ 보스 오브젝트 활성화 → 보스 스프라이트/스탯 세팅 → 전투 시작
        └─ 아니오: [일반 전투]
             └─ Enemy N개 초기화 → 스프라이트/스탯 세팅 → 전투 시작
```

### B) 전투 진행(타이밍 기반)

```
[애니메이션 클립]
    └─ Event(AttackDamages)
         └─ BattleContentController.AniEventAttackDamages()
              └─ 데미지 계산/적용 → HP/상태 갱신 → 처치 시 보상 처리
```

### C) 파견

```
[파견 UI]
  └─ Dispatch 리스트 로드(난이도별)
      └─ 선택: 시간·성공확률·골드·XP 미리보기
           └─ 진행/완료 → 골드/XP 반영 → 결과 로그
```

### D) 성장(훈련)

```
[훈련 UI]
  └─ 단계 선택(0~3)
       └─ 공/방/체 중 택1 → 시간·비용·성공확률 표시
            └─ 진행 → 성공 시 스탯 증가 / 실패 시 재도전
```

---

## 9) 주요 스크립트 기능 요약

* **BattleCharctreController.cs**

  * 전투 씬 루트(플레이어/Enemy/보스) 참조 캐싱
  * 보스전/일반전 분기 초기화, 스프라이트/스탯 주입(DataBase/ContentController 연동)
  * 플레이어 아웃라인 일괄 제어, 개별 I/F 접근자(getter) 제공
* **BattleEffecController.cs**

  * AttackArea/DontClick(입력 차단 영역) 참조 초기화 및 상태 제어
* **BattleAniEvent.cs**

  * 애니 이벤트 → BattleContentController 공격 타이밍 콜백 호출

---

## 10) 포트폴리오용 README 핵심 섹션 제안

* **프로젝트 한 줄 소개**: “데이터 주도 설계로 전투·파견·성장을 엮은 용병단 시뮬레이션”
* **핵심 기여**: 전투 초기화 파이프라인, 인터페이스 기반 전투 객체 관리, Resources 데이터 로더
* **기술 포인트**

  1. **데이터 주도 설계**: JSON/TXT 테이블로 밸런싱 유연화
  2. **인터페이스/모듈화**: 전투 오브젝트 공통 I/F 정의 → 테스트/교체 용이
  3. **애니메이션 이벤트 연동**: 타이밍 정확도↑, 데미지 계산 일원화
* **플레이 영상/스크린샷**: (추가) 전투 장면·파견 UI·훈련 UI
* **폴더 구조/의존도 다이어그램**: (추가) Scripts / Resources / Scenes
* **향후 개선**

  * ScriptableObject 전환(빌드 크기/로드 최적화)
  * Addressables 도입(에셋 메모리 관리/패치 파이프라인)
  * 전투 로그/리플레이(디버깅/밸런싱 툴)

---

## 11) 테스트 시나리오(샘플)

1. **일반 전투**: 적 인덱스 X 선택 → 전투 입장 → 애니 이벤트 타이밍 데미지 확인 → 처치 보상/XP 반영
2. **보스 전투**: 보스 선택 → 보스 스프라이트/스탯 일치 확인 → 처치 시 스테이지 XP 테이블 값 적용
3. **파견**: 난이도 2 과제 선택 → 시간 경과 후 성공/실패 처리 및 골드/XP 테이블 반영 확인
4. **훈련**: 단계 3, 공격력 선택 → 성공 시 stats 증가 및 비용/시간 로그 확인

---

## 12) 마무리

* 본 프로젝트는 **데이터 중심 설계**를 통해 **전투/파견/성장**의 세 시스템을 일관성 있게 연결하고,
* **인터페이스 기반 전투 객체 관리**와 **애니메이션 이벤트 콜백**으로 타이밍 기반 전투 로직을 안정화했습니다.

> 추가 자료(영상 링크, 캡처)와 함께 README 배포본으로 다듬을 수 있도록 섹션별 텍스트를 제공합니다. 추후 원하시면 GitHub용 마크다운/이미지 배치까지 함께 정리해 드립니다.
