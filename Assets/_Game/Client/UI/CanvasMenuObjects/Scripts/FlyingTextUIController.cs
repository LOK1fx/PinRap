using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasScripts
{
    public class FlyingTextUIController : MonoBehaviour
    {
        [SerializeField] private Text textPrefab;
        [SerializeField] private float textSpeed;
        [SerializeField] private float textWait;
        public static FlyingTextUIController Instance;
        private Vector3 _startPosition;
        private Vector3 _activePosition;
        private Vector3 _endPosition;
        public static Transform CanvasTransform;

        private void Start()
        {
            Instance = this;
            _endPosition = new Vector3(Screen.width / 2, Screen.height + Screen.height / 8, 0);
            _startPosition = new Vector3(Screen.width / 2, Screen.height + Screen.height / 8, 0);
            _activePosition = new Vector3(Screen.width / 2, Screen.height - Screen.height / 8, 0);
            DontDestroyOnLoad(gameObject);
            textSpeed = textSpeed * Screen.height / 100f;
        }

        public Text SpawnFlyingTextAndAfterDelete(string text)
        {
            var newActiveText = Instantiate(textPrefab, CanvasTransform);
            newActiveText.transform.position = _startPosition;
            newActiveText.text = text;
            StartCoroutine(
                MoveFlyingText(_activePosition, newActiveText.transform, 
                    (transform1) => MoveToEndAndDie(transform1, textWait)));
            return newActiveText;
        }

        public void MoveToEndAndDie(Transform activeText, float waitTime)
        {
            StartCoroutine(WaitSomeTime(waitTime, () =>
            {
                StartCoroutine(MoveFlyingText(_endPosition, activeText, transform1 =>
                {
                    Destroy(transform1.gameObject);
                }));
            }));
        }

        public IEnumerator WaitSomeTime(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
        
        public Text SpawnFlyingText(string text, Action<Transform> onEndMove)
        {
            var newActiveText = Instantiate(textPrefab, CanvasTransform);
            newActiveText.transform.position = _startPosition;
            newActiveText.text = text;
            StartCoroutine(
                MoveFlyingText(
                    new Vector3(Screen.width / 2, Screen.height - Screen.height / 8, 0), 
                    newActiveText.transform,
                    onEndMove)
            );
            return newActiveText;
        }

        private void MoveEndedFlyingText(Transform activeText, Action<Transform> onEndMove)
        {
            StartCoroutine(MoveEndFlyingText(activeText, onEndMove));
        }

        private IEnumerator MoveEndFlyingText(Transform activeText, Action<Transform> onEndMove)
        {
            yield return new WaitForSeconds(textWait);
            Debug.Log("wait");
            yield return MoveFlyingText(
                new Vector3(Screen.width / 2, Screen.height + Screen.height / 8, 0), 
                activeText, 
                onEndMove);
        }

        public IEnumerator MoveFlyingText(Vector3 nextPosition, Transform activeText, Action<Transform> onEnd)
        {
            yield return MoveFlyingText(nextPosition, activeText);
            onEnd.Invoke(activeText);
        }
        
        public IEnumerator MoveFlyingText(Vector3 nextPosition, Transform activeText)
        {
            var constForce = Vector3.Normalize(nextPosition - activeText.position);
            var distance = Vector2.Distance(nextPosition, activeText.position);
            while (distance >= Vector2.Distance(nextPosition, activeText.position))
            {
                distance = Vector2.Distance(nextPosition, activeText.position);
                activeText.position += constForce * (Time.deltaTime * textSpeed);
                yield return null;
            }

            activeText.position = nextPosition;
        }
    }
}