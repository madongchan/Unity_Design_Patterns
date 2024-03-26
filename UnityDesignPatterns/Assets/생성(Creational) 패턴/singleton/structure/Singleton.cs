using UnityEngine;

namespace NG.Patterns.Structure.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // 지정된 클래스 타입의 인스턴스
        private static T _instance;

        // _instance가 한 스레드에 의해 설정되기 시작하면 다른 스레드가 수정하는 것을 방지하는 락 객체
        private static object _instLock = new object();

        // 싱글턴이 제거/파괴되는 과정에 접근하는 것을 방지하는 불리언 변수
        bool _disposing = false;

        // 다른 클래스에서 싱글턴에 접근할 수 있도록 하는 공개 접근자. <클래스명>.Instance로 타이핑하여 접근
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 인스턴스가 없으면 찾아서 설정한다.
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }

            set
            {
                lock (_instLock) // 스레드 안전성 보장
                {
                    if (_instance == null)  // _instance가 null일 때만 설정
                        _instance = value;
                    else if (_instance.Equals(value)) // 현재 Instance만 _instance 변수를 수정할 수 있음(예를 들어, null로 설정)
                        _instance = value;
                }
            }
        }

        // 프로그래머에게 함수를 오버라이드해야 함을 알리기 위해 가상 함수로 만듬
        // 프로그래머가 호출을 완전히 오버라이드할지, 아니면 새로운 기능과 함께 기본 호출을 포함할지 결정할 수 있도록 함.
        // C#에서는 새 클래스에서 base.Awake()를 호출함으로써 이를 수행함
        public virtual void Awake()
        {
            if (Instance == null)
                Instance = (T)this;
            else if (Instance != this)  // 이미 이 클래스의 인스턴스가 있는지 확인
            {
                // 이미 이 클래스의 인스턴스가 있다면, 방금 생성된 객체를 파괴한다.
                Debug.LogWarning("한 개 이상의 싱글턴 객체를 생성하려고 시도함. 새로운 인스턴스를 파괴함 " + gameObject.ToString() + "\n한 개 이상의 인스턴스를 원한다면, 작성한 클래스가 싱글턴 클래스를 상속받지 않도록 하세요.");
                Dispose();
            }
        }

        // 싱글턴 인스턴스의 적절한 청소는 가비지 컬렉션을 허용하기 위해 참조를 null로 설정하는 것을 포함함
        protected virtual void Dispose()
        {
            _disposing = true;
            Instance = null;

            Destroy(gameObject);
        }

        // 부적절한 방식으로 파괴된 경우
        public virtual void OnDestroy()
        {
            if (!_disposing)
                _disposing = true;

            if (Instance != null)
                Instance = null;

            Debug.Log($"Singleton {gameObject.name} has been destroyed.");
        }
    }
}
