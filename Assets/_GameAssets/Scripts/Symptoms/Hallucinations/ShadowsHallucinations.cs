using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class ShadowsHallucinations : Symptoms
    {
        // Provided by the preset
        public List<ImageSO> hallucinationImages;

        private Transform[] rendererGOs;
        private Coroutine[] coroutines;


        // Start is called before the first frame update
        void Start()
        {
            Random.InitState(Constants.RANDOM_SEED);
            rendererGOs = GetComponentsInChildren<Transform>();
            coroutines = new Coroutine[rendererGOs.Length];
        }


        private IEnumerator SimulateCoroutine(Transform rendererGO)
        {
            while (IsSimulating)
            {
                SpriteRenderer mainSR = rendererGO.Find("Main").GetComponent<SpriteRenderer>();
                SpriteRenderer distortSR = rendererGO.Find("Distort").GetComponent<SpriteRenderer>();

                int index = Random.Range(0, hallucinationImages.Count);
                ShadowHallucinationSO visualHallucination = hallucinationImages[index] as ShadowHallucinationSO;

                rendererGO.transform.localScale = Vector3.one * visualHallucination.scale;
                mainSR.sprite = visualHallucination._sprite;
                distortSR.sprite = visualHallucination._sprite;
                distortSR.GetComponent<Renderer>().material = visualHallucination._material;


                float distance = Random.Range(visualHallucination.distance.min, visualHallucination.distance.max) + Random.value;
                float angle = Random.Range(-60, 60) + Random.value;
                Vector3 pos = Quaternion.Euler(0, angle, 0) * Character.transform.forward;
                pos *= distance;

                rendererGO.transform.position = pos;

                float interval = Random.Range(visualHallucination.timeAfterClip.min, visualHallucination.timeAfterClip.max) + Random.value;
                yield return new WaitForSeconds(interval);

                mainSR.enabled = false;
                distortSR.enabled = false;
            }
            yield return new WaitForEndOfFrame();
        }


        public override void Simulate()
        {
            IsSimulating = true;
            for (int i = 0; i < rendererGOs.Length; i++)
            {
                Coroutine coroutine = StartCoroutine(SimulateCoroutine(rendererGOs[i]));
                coroutines[i] = coroutine;
            }
        }

        public override void Stop()
        {
            foreach (Coroutine coroutine in coroutines)
            {
                StopCoroutine(coroutine);
            }
            IsSimulating = false;
        }
    }
}