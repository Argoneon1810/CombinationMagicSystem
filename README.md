# Combination Magic System
이 프로젝트는 바텀업 방식의 마법 스킬 시스템을 구현하기 위해 시작되었습니다.

바텀업 방식의 마법이란 마법이 완성된 형태로 제공되는 것이 아니라 마법의 구성자를 제공하고 플레이어가 자유롭게 조합할 수 있도록 하여 플레이어가 마법을 완성하는 것을 말합니다.

구상 계획에 대한 상세한 내용은 [Assets/ReadMe.md](Assets/ReadMe.md) 을 확인해 주세요.

# Demo
## Radial Stat Pannel
현재 개발 단계에서 마법의 연구나 코스트, 데미지량에 관여할 것으로 계획한 스탯은 총 5가지가 있지만, 개발이 진행됨에 따라 추가되거나 제거될 수 있습니다.

따라서 스탯의 종류가 많아지거나 적어져도 시각적으로 표시가 가능한 UI를 만들기로 하였습니다.

### 스탯의 구현과 표시 방식
#### Stats.cs
신이 시작되면 `ScriptableObject`인 `StatElement`들을 Wrapper Class인 `ProgressableStatElement`로 감싸 고정 수치와 구분되는 가변 수치를 가질 수 있게 합니다.

##### Wrapper Class 사용 이유
`StatElement`를 `ProgressableStatElement`라는 Wrapper Class로 감싸는 것이 불필요하거나 과해 보일 수 있습니다.

실제로 `StatElement`를 단순한 컴포넌트로 만든 후 `GameObject` 통째로 Prefab으로 만들었어도 비슷한 결과를 얻을 수 있었습니다. 그러나 이렇게 하면 몇가지 문제가 있었습니다.

1. 중앙 통제가 어렵습니다. Project 패널에서 Prefab의 값을 수정해도 신에 이미 추가되어 있는 스탯들은 영향을 받지 않습니다.
2. 각 스탯 별로 신에 `GameObject`가 생성되므로 불필요한 낭비임에 더해 Scene Hierarchy도 지저분해집니다.

플레이어가 스탯을 쌓은 크기를 가중치라고 표현하자면, 가중치가 적용되기 전의 기본 스탯은 항상 동일해야 합니다. 이것에 유리한 기능이 `ScriptableObject`이므로 각 스탯의 가중치만 관리할 Wrapper Class를 사용할 것으로 최종 결정하였습니다.

##### 값 변화 감지
`Start` 이벤트 메소드에서 `StatElement`를 `ProgressableStatElement`로 감쌀 때 각 스탯 항목에 관찰자 스레드를 붙입니다.

이 스레드는 0.1초마다 `ProgressableStatElement`의 식 본문 멤버 `float Progress`를 읽어들이고 값이 이전과 다른 경우 `public event Action ValueChangedCallback`를 실행하는 메소드 `void OnValueChanged()`를 메인 스레드에서 호출하도록 `UnityMainThreadDispatcher.Enqueue(IEnumerator action)` 합니다.

#### RadialUIPolygon.cs
이 클래스는 다각형 스탯 패널의 형태를 구현하는 컴포넌트로, `UnityEngine.UI.Graphic`을 상속합니다.

다각형의 꼭짓점의 개수, 각 꼭짓점이 중앙으로부터 떨어진 크기, 다각형의 전체적인 회전 이 세가지를 제어할 수 있는 필드를 가지고 있습니다.

#### StatGUI.cs
[RadialUIPolygon](RadialUIPolygon.cs)을 상속하는 이 클래스는 다각형의 꼭짓점 위치에 라벨을 표시하기 위해 만들어졌습니다.

`TMPro.TextMeshProUGUI`의 rectTransform들을 꼭짓점 위치 + 패딩 위치로 옮기는 역할을 수행하며, 꼭짓점 위치들은 부모 클래스로부터 가져옵니다.

#### StatVisualizer
[Stats](Stats.cs)에서 `Pair<string, float>[] GetStats()`를 호출하면 모든 스탯의 이름과 값을 받아올 수 있습니다. 이 컴포넌트는 이렇게 읽어들인 값들을 StatGUI와 RadialUIPolygon에게 전달하는 역할을 합니다.

`Pair<string, float>[] Normalize(Pair<string, float>[] statsPairArray)` 메소드를 사용해 위 함수의 반환값 중 가장 큰 값을 1로 하여 나머지를 \[0\~1\] 범위로 정리할 수 있습니다. Normalize 여부는 필드 `bool normalize`를 사용해 간편히 끄고 켤 수 있습니다.

#### StatPanelController
StatGUI와 별개로 배경용으로 쓰는 RadialUIPolygon이 있어 둘 모두를 한꺼번에 컨트롤 하기 쉽게끔 만들어진 컴포넌트입니다.

패널의 회전 및 색상, 라벨의 폰트 크기, 색상 꼭짓점에서 라벨까지의 패딩 크기 등을 컨트롤 할 수 있습니다.

### 예
#### Normalized
![Normalized](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/Radial%20Normalized.gif)

#### Unnormalized
![Unnormalized](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/Radial%20Unnormalized.gif)

#### 기타 조작
![Control](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/Radial%20Control.gif)

## Quick Access Keys (WIP)
마법을 이미 구성하여 저장해 뒀을 때 단축키에 등록하여 사용할 수 있게끔 하기 위해 만들어진 컴포넌트입니다.

키가 눌리면 `public UnityEngine.Events.UnityEvent OnKeyDown`을 실행합니다.

### 예
#### Key Being Pressed
![KeyBinder](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/KeyBinder%20QWER.gif)

## Skill Maker (WIP)
*/\*GUI 작업이 필요한 부분이라 아직 인스펙터로만 조작이 가능합니다\*/*

다음 플레이 스크립트들을 충족할 것을 전제로 작업했습니다:
1. "어떤 버튼 등을 클릭해 새 마법 생성을 시작했을 때 마법 생성이 시작되어야 한다."
2. "생성 작업 도중에는 명시적인 종료 없이 새 마법의 생성이 시작되어서는 안된다."
3. "마법의 슬롯 크기가 N이라고 지정할 수 있어야 한다."
4. "마법이 슬롯을 다 채우지 못해도 마법으로써 성립한다면 저장할 수 있어야 한다."
5. "마법 슬롯 크기가 여유로워도 마법으로써 성립하지 않는다면 저장할 수 없어야 한다."
6. "이미 추가된 마법은 다시 추가되어서는 안된다."
7. "저장한 마법은 플레이어가 사용하길 원할 때 꺼내 쓸 수 있어야 한다."

### MagicConstructor
`ScriptableObject`로 마법이름, 슬롯용량, ban 리스트, requisite 리스트를 가집니다.

ban 리스트는 이 마법 구성자가 포함된 마법이 동시에 가질 수 없는 다른 구성자를 나타냅니다.

requisite 리스트는 이 마법 구성자가 포함되기 위해 반드시 포함해야 하는 다른 구성자를 나타냅니다.

### MemorizeMagic
`MagicConstructor`들을 보관할 Wrapper Class입니다.

요청이 들어오면 용량이 충분한지, 추가하려는 마법이 밴 되어 있지는 않은지, 마법이 성립하는지 등의 판단을 수행합니다.

### DebugSkillMaker
1\~3 플레이 스크립트의 구현을 목적으로 하는 컴포넌트입니다.

- 이미 생성 도중인 마법이 없다면 대기중인 마법 `MemorizeMagic`을 생성합니다.
- 디버그 토글을 통해 두개의 예안 마법 구성자를 대기중인 마법에 추가합니다.
	- 첫번째 마법 구성자는 `Projectilefy`로 `Rigidbodyfy` 구성자를 requisite로 가집니다. 슬롯용량은 1입니다.
	- 두번째 마법 구성자는 `Rigidbodyfy`로 requisite은 없습니다. 슬롯용량은 1입니다.
- 추가하려는 마법이 requisite를 가지고 있으면 이것도 함께 추가합니다. 무엇을 추가하는데 실패했는지 추적이 가능합니다.
- 대기중인 마법을 슬롯으로 추가하려고 할 때 마법이 성립하는지 판단하고 성립하는 경우 추가합니다. 대기중인 마법은 `null`로 합니다.

### SkillSlot
완성된 마법 `MemorizeMagic`을 저장하는 역할을 합니다.

실제 게임에서 단축키 조작 대신 스킬 창을 열어 스킬을 사용할 수도 있으므로 간단히 스스로도 마법을 실행할 수 있도록 구현되어 있습니다.

### MagicExtraScript
`MonoBehaviour`를 상속하는 컴포넌트로, 마법의 실제 동작 내용을 정의합니다.

후술할 `MagicRealizer`가 `MagicConstructor`에 맞춰 게임오브젝트에 이 컴포넌트의 자녀 클래스들을 추가하게 됩니다.

예를 들어 `MagicRealizer`는 `class Rigidbodyfy : MagicConstructor`를 확인하면 게임오브젝트에 `class RigidbodyfyMagicExtra : MagicExtraScript`를 추가합니다.

### MagicRealizer
`MemorizeMagic`을 전달받아 `MagicCaster`로부터 시전된 마법이 소환될 위치를 받아온 후 해당 위치에 실제 마법이 될 `GameObject` 인스턴스를 만듭니다.

`MagicConstructor`가 `ScriptableObject`를 상속하는 관계로 스스로 `gameObject`에 대한 접근을 가지고 있지 않아 필요에 의해 만들어졌습니다.

### 예
#### 슬롯 용량이 충분할 때
아래 예는 `Rigidbodyfy` 구성자를 Requisite로 요구하는 `Projectilefy` 구성자를 추가할 때 슬롯 용량이 충분하여 자동으로 `Rigidbodyfy` 구성자가 추가되는 모습을 보여줍니다.

Requisite이 따로 없는 경우 자기 자신만 추가합니다.

Requisite이 충족되었으므로 스킬 슬롯으로 저장이 가능합니다.

![Sufficient](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/SkillMaker%20Enough%20Capacity.gif)

#### 슬롯 용량이 충분하지 않을 때
앞 예 동일한 조건에서 슬롯 용량이 충분하지 못해 `Rigidbodyfy` 구성자가 추가되지 못하는 상황을 보여줍니다.

Requisite이 충족되지 못했으므로 스킬 슬롯으로 저장이 불가능합니다.

![Insufficient](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/SkillMaker%20Not%20Enough%20Capacity%20or%20Requisite%20Not%20Met.gif)

#### 구성자를 중복 등록하고자 할 때
구성자는 항상 자기 자신을 ban 리스트에 포함하고 있습니다. 따라서 구성자가 한번 등록되면 같은 구성자를 재등록할 수 없게 됩니다.

![Duplicate](https://github.com/Argoneon1810/CombinationMagicSystem/blob/399090d3f2a091e4d2e05c5f0db83bc11f1583e6/gifs/SkillMaker%20Try%20Duplicate.gif)
