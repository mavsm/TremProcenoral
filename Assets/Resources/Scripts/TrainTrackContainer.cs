using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrackContainer : MonoBehaviour
{
    [SerializeField] private Transform trainFront;
    [SerializeField] private Transform trainTransform;
    

    [SerializeField] private List<GameObject> tracks = new List<GameObject>();

    [SerializeField] private float timeBetweenMoves = 0.5f;

    private int currentLastTrackIndex = 0;
    private bool isMoving = false;
    private Coroutine movingCoroutine;

    private void Start(){
        // print(transform.position);
        Events.instance.onTrainStoppedMoving += TrainStopMoving;
        Events.instance.onFloorClicked += TrainBegunMoving;
    }

    private void OnDestroy(){
        Events.instance.onTrainStoppedMoving -= TrainStopMoving;
        Events.instance.onFloorClicked -= TrainBegunMoving;
    }


    private void TrainBegunMoving(Vector3 pos){
        if(isMoving) return;
        isMoving = true;
        movingCoroutine = StartCoroutine(TrainMovingCoroutine());
    }

    private void TrainStopMoving(){
        isMoving = false;
        if(movingCoroutine != null) StopCoroutine(movingCoroutine);
    }

    private IEnumerator TrainMovingCoroutine(){
        yield return null; //idle frame
        SetNextTrack();
        while(isMoving){
            yield return new WaitForSeconds(timeBetweenMoves);
            SetNextTrack();
        }
    }

    private void SetNextTrack(){
        GameObject lastTrack = tracks[currentLastTrackIndex];

        Vector3 newPos = trainFront.position;
        newPos.y = lastTrack.transform.position.y;
        lastTrack.transform.position = newPos;
        lastTrack.transform.rotation = trainTransform.rotation;

        currentLastTrackIndex = (currentLastTrackIndex+1)%tracks.Count;
    }

}
