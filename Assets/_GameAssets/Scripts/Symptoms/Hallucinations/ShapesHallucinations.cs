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
        private Coroutine[] coroutines;



        protected override void Awake()
        {
            base.Awake();
            rendererGOs = new Transform[transform.childCount];
            for (int i = 0; i < rendererGOs.Length; i++)
            {
                rendererGOs[i] = transform.GetChild(i);
            }

            coroutines = new Coroutine[rendererGOs.Length];
        }


        // Start is called before the first frame update
        private void Start()
        {
            Simulate();
        }

        private IEnumerator SimulateCoroutine(Transform rendererGO)
        {
            SpriteRenderer mainSR = rendererGO.Find("Main").GetComponent<SpriteRenderer>();
            SpriteRenderer distortSR = rendererGO.Find("Distort").GetComponent<SpriteRenderer>();
            while (IsSimulating)
            {
                float interval = Random.Range(shapesSO.activeTime.min, shapesSO.activeTime.max) + Random.value;

                if (ShouldSimulate(shapesSO))
                    yield return PlaySimulation(rendererGO, mainSR, distortSR, interval);
                else
                    yield return new WaitForSeconds(interval);
            }
            yield return new WaitForEndOfFrame();
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
            Vector3 pos = Quaternion.Euler(0, angle, 0) * characterData.CharacterController.transform.forward;
            pos *= distance;

            rendererGO.position = characterData.CharacterCenter + pos;
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


        public override void Simulate()
        {
            IsSimulating = true;
            for (int i = 0; i < rendererGOs.Length; i++)
            {
                print(rendererGOs[i].name);
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