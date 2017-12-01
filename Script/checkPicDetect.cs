using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class checkPicDetect : MonoBehaviour, ITrackableEventHandler
{
    private GameObject objects;
    private TrackableBehaviour mTrackableBehaviour;
    void Start()
    {
        objects = GetComponent<GameObject>();
        objects.SetActive(false);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
   
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            objects.SetActive(true);
        }
        else
        {
            objects.SetActive(false);
        }
    }
}
