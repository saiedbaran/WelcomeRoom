using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using TMPro;


namespace WelcomeRoom.QuestManager
{
    public class QI_Teleportation : MonoBehaviour
    {
        [SerializeField] int MaxTryNumber = 3;
        public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
        private TeleportMarkerBase teleportingToMarker;
        public QI_Teleportation_Helper[] Helpers;
        int _tryNumber = 0;

        private List<Quest> subquestList = new List<Quest>();

        private AudioSource _audioSource;
        private List<AudioClip> _audioClips = new List<AudioClip>();
        void Start()
        {
            teleportAction = Teleport.instance.teleportAction;
            Helpers = FindObjectsOfType<QI_Teleportation_Helper>();
            foreach (var helper in Helpers)
            {
                helper.HelperObject.SetActive(true);
            }

            AudioManagement();
            PlayFirstAudio();
        }

        void Update()
        {
            if (gameObject.GetComponent<SubQuest>().IsActive)
            {
                if (Helpers.Length == 0)
                {
                    Helpers = FindObjectsOfType<QI_Teleportation_Helper>();
                    foreach (var helper in Helpers)
                    {
                        helper.HelperObject.SetActive(true);
                    }
                    AudioManagement();
                }

                if(!_audioSource.isPlaying) { PlayFirstAudio();}

                if (teleportAction.stateUp)
                {
                    _tryNumber++;
                    ModifyText();
                }

                if (_tryNumber >= MaxTryNumber)
                {
                    QuestDone();
                }
            }
        }

        private void ModifyText()
        {
            foreach (var helper in Helpers)
            {
                if (helper.Current.GetComponent<TextMeshPro>())
                { helper.Current.GetComponent<TextMeshPro>().text = _tryNumber.ToString(); }
                if (helper.Total.GetComponent<TextMeshPro>())
                { helper.Total.GetComponent<TextMeshPro>().text = "/ " + MaxTryNumber + " repeat"; }
            }
        }

        private void QuestDone()
        {
            Debug.Log("Teleport Action Changed!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            //ActiveNextQuest();

            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }

        public void AudioManagement()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();

            foreach (var helper in Helpers)
            {
                foreach (var clip in helper.AudioClips)
                {
                    _audioClips.Add(clip);
                }
            }
        }

        public void PlayFirstAudio()
        {
            _audioSource.clip = _audioClips[0];
            _audioSource.Play();
        }
    }
}
