using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{
    //Every time we go to next segment, we'll spawn/turn on all of these objects
    [System.Serializable]
    public class Message
    {
        public AudioClip voiceOver;
        //GameObjects in the scene to turn on
        public GameObject textMessage;
        public GameObject oneHandedControlMessage;
        public float timeToNext = -1;
    }

    public List<Message> segments;

    //After timeToNext time, go to next segment
    protected float timeToNext
    {
        get
        {
            float defaultF = 6f;
            if(curSegment<0 || curSegment >= segments.Count)
            {
                return defaultF;
            }
            float time = segments[curSegment].timeToNext;
            if(time==-1)
            {
                return defaultF;
            }
            else
            {
                return time;
            }
        }
    }
    protected float timeSoFar = 0.0f;

    protected int curSegment = -1;

    //random references needed
    protected GameObject curTextMessage;
    protected AudioSource audioSource;

    protected void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(audioSource==null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        GoToNext();
    }

    protected void Update()
    {
        timeSoFar += Time.deltaTime;
        if(timeSoFar>=timeToNext)
        {
            GoToNext();
            timeSoFar = 0;
        }
    }

    protected void GoToNext()
    {
        //destroy current message
        if(curTextMessage!=null)
        {
            GameObject.Destroy(curTextMessage);
        }

        //increase our segment
        curSegment += 1;
        if(curSegment>=segments.Count)
        {
            //we're done, probably do stuff
            return;
        }

        //Get the current segment and play/spawn all of its things
        Message segment = segments[curSegment];
        if(segment.voiceOver!=null)
        {
            audioSource.PlayOneShot(segment.voiceOver);
        }
        if(segment.oneHandedControlMessage!=null && ControlScheme.isOneHanded)
        {
            segment.oneHandedControlMessage.SetActive(true);
            curTextMessage = segment.oneHandedControlMessage;
        }
        else if(segment.textMessage!=null)
        {
            segment.textMessage.SetActive(true);
            curTextMessage = segment.textMessage;
        }
    }
}
