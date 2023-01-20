using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class ShapesHallucinations : Symptoms
    {
        // Provided by the preset
        public ShapesSO shapesSO;

        private Transform[] rendererGOs;

        protected override void Awake()
        {
            base.Awake();
            rendererGOs = new Transform[transform.childCount];
            for (int i = 0; i < rendererGOs.Length; i++)
                rendererGOs[i] = transform.GetChild(i);
        }

        // Start is called before the first frame update
        private void Start()
        {
            Simulate();
        }

        protected override void Prepare()
        {
            base.Prepare();
            for (int i = 0; i < rendererGOs.Length; i++)
                rendererGOs[i].gameObject.SetActive(true);

        }

        protected override void CleanUp()
        {
            for (int i = 0; i < rendererGOs.Length; i++)
                rendererGOs[i].gameObject.SetActive(false);
            base.CleanUp();
        }

        protected override IEnumerator MasterCoroutine()
        {
            Prepare();
            while (isMasterCoroutineRunning)
            {
                for (int i = 0; i < rendererGOs.Length; i++)
                {
                    SpriteRenderer mainSR = rendererGOs[i].Find("Main").GetComponent<SpriteRenderer>();
                    SpriteRenderer distortSR = rendererGOs[i].Find("Distort").GetComponent<SpriteRenderer>();
                    StartCoroutine(RunProbabilityTest(rendererGOs[i], mainSR, distortSR));
                }
            }
            yield return new WaitForEndOfFrame();
            CleanUp();
        }

        private IEnumerator RunProbabilityTest(Transform rendererGO, SpriteRenderer mainSR, SpriteRenderer distortSR)
        {
            float interval = Random.Range(shapesSO.activeTime.min, shapesSO.activeTime.max) + Random.value;
            if (ShouldSimulate(shapesSO))
                yield return PlaySimulation(rendererGO, mainSR, distortSR, interval);
            else
                yield return new WaitForSeconds(interval);
        }


        private IEnumerator PlaySimulation(Transform rendererGO, SpriteRenderer mainSR, SpriteRenderer distortSR, float interval)
        {
            int index = Random.Range(0, shapesSO.sprites.Count);

            mainSR.sprite = shapesSO.sprites[index];
            distortSR.sprite = shapesSO.sprites[index];

            distortSR.GetComponent<Renderer>().material = shapesSO._materials[Random.Range(0, shapesSO._materials.Length)];

            float scale = Random.Range(shapesSO.scale.min, shapesSO.scale.max);
            float distance = Random.Range(shapesSO.distance.min, shapesSO.distance.max) + Random.value;
            float angle = Random.Range(40, 60);
            angle *= Random.value > 0.5 ? 1 : -1;
            float transparencySR = Random.Range(shapesSO.transparency.min, shapesSO.transparency.max);
            float transparencyDistort = Random.Range(shapesSO.transparency.min, shapesSO.transparency.max);
            Vector3 pos = Quaternion.Euler(0, angle, 0) * CharacterData.CharacterController.transform.forward;
            pos *= distance;

            rendererGO.position = CharacterData.CharacterCenter + pos;
            rendererGO.localScale = Vector3.one * scale;

            mainSR.color = new Color(mainSR.color.r, mainSR.color.g, mainSR.color.b, transparencySR);
            distortSR.color = new Color(distortSR.color.r, distortSR.color.g, distortSR.color.b, transparencyDistort);

            yield return new WaitForSeconds(interval - 1);
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                float currentColor = Tweens.EaseInExponential(time);

                mainSR.color = new Color(mainSR.color.r, mainSR.color.g, mainSR.color.b, transparencySR * (1 - currentColor));
                distortSR.color = new Color(distortSR.color.r, distortSR.color.g, distortSR.color.b, transparencyDistort * (1 - currentColor));
                yield return null;
            }

            mainSR.color = new Color(mainSR.color.r, mainSR.color.g, mainSR.color.b, 0);
            distortSR.color = new Color(distortSR.color.r, distortSR.color.g, distortSR.color.b, 0);
        }


    }
}