using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public class QI_MovePhys : MonoBehaviour, IQuest
    {
        [SerializeField] float MovementThreshold = 10f;

        Vector3 _iniCameraPosition;
        Vector3 _prevCameraposition;
        int _runCycle;

        public float SumDistance = 0f;
        public QI_MovePhys_Helper[] Helpers;

        private AudioSource _audioSource;
        private List<AudioClip> _audioClips = new List<AudioClip>();

        void Start()
        {
            _prevCameraposition = Camera.main.transform.position;

            Helpers = FindObjectsOfType<QI_MovePhys_Helper>();
            foreach (var helper in Helpers)
            {
                helper.HelperObject.SetActive(true);

                foreach (var clip in helper.AudioClips)
                {
                    _audioClips.Add(clip);
                }
            }

            //_audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource = gameObject.GetComponent<AudioSource>();
            _audioSource.clip = _audioClips[0];
            _audioSource.Play();
        }

        void Update()
        {
            if (gameObject.GetComponent<SubQuest>().IsActive)
            {
                SumDistance += (Camera.main.transform.position - _prevCameraposition).magnitude;
                _prevCameraposition = Camera.main.transform.position;

                ModifyText();

                if (SumDistance > (MovementThreshold * 0.8f) && _audioSource.clip != _audioClips[1])
                {
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[1];
                    _audioSource.Play();
                }

                if (SumDistance > MovementThreshold)
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
                { helper.Current.GetComponent<TextMeshPro>().text = (int)(SumDistance * 100) + " cm"; }
                if (helper.Total.GetComponent<TextMeshPro>())
                { helper.Total.GetComponent<TextMeshPro>().text = "/ " + (int)(MovementThreshold * 100) + " cm"; }
            }
        }

        private void QuestDone()
        {
            Debug.Log("Ok, you walked enough!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }
    }
}
