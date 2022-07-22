using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasScripts
{
    public class FlyingTextUIController : MonoBehaviour
    {
        [SerializeField] private Text textPrefab;
        [SerializeField] private float textSpeed;
        public static Transform CanvasTransform;
        public static Text ActiveText;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            textSpeed = textSpeed * Screen.height / 100f;
        }

        public static void SpawnFlyingTextAndAfterDelete(string text)
        {
            var controller = FindObjectOfType<FlyingTextUIController>();
            controller._SpawnFlyingTextAndAfterDelete(text);
        }

        private void _SpawnFlyingTextAndAfterDelete(string text)
        {
            ActiveText = Instantiate(textPrefab, CanvasTransform, true);
            ActiveText.transform.position = new Vector3(Screen.width / 2, Screen.height + 200, 0);
            ActiveText.text = text;
            StartCoroutine(
                MoveFlyingText(new Vector3(Screen.width / 2, Screen.height - Screen.height / 7, 0))
            );
        }

        public void MoveEndedFlyingText()
        {
            if (ActiveText.transform.position.y < 0)
            {
                Destroy(ActiveText.gameObject);
            }
            else
            {
                Invoke(nameof(MoveEndFlyingText), 5f);
            }
        }

        public void MoveEndFlyingText()
        {
            StartCoroutine(MoveFlyingText(new Vector3(Screen.width / 2, Screen.height + 200, 0)));
        }

        public IEnumerator MoveFlyingText(Vector3 nextPosition)
        {
            var constForce = Vector3.Normalize(ActiveText.transform.position - nextPosition);
            while (Vector2.Distance(nextPosition, ActiveText.transform.position) > 0.1)
            {
                ActiveText.transform.position += constForce * Time.deltaTime;
                yield return null;
            }

            ActiveText.transform.position = nextPosition;
            MoveEndedFlyingText();
        }
    }
}