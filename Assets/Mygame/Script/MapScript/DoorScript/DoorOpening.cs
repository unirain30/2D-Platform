using UnityEngine;

public class DoorOpening : MonoBehaviour
{

    [SerializeField] private float targetPositionX;
    [SerializeField] private float targetPositionY;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    private Vector3 targetPosition;
    private Vector3 iniPosition;
    private float elapsedTime;
    public bool isOpening = false;

    private void Awake() {
        targetPosition = new Vector3(targetPositionX, targetPositionY, 0f);
        iniPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isOpening){
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / duration;
            transform.position = Vector3.Lerp(iniPosition, targetPosition, curve.Evaluate(percent));
        }
        if(transform.position == targetPosition){
            isOpening = false;
        }
    }
}
