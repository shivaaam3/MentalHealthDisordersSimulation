using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sharmas4.MentalHealthDisorder
{
    public class TvHallucinations : ShapesHallucinations
    {
        protected override IEnumerator PlaySimulation(Transform rendererGO, SpriteRenderer mainSR, SpriteRenderer distortSR, float interval)
        {
            print("Called");
            int index = Random.Range(0, shapesSO.sprites.Count);

            mainSR.sprite = shapesSO.sprites[index];
            distortSR.sprite = shapesSO.sprites[index];

            distortSR.GetComponent<Renderer>().material = shapesSO._materials[Random.Range(0, shapesSO._materials.Length)];

            float transparencySR = Random.Range(shapesSO.transparency.min, shapesSO.transparency.max);
            float transparencyDistort = Random.Range(shapesSO.transparency.min, shapesSO.transparency.max);

            mainSR.color = new Color(mainSR.color.r, mainSR.color.g, mainSR.color.b, transparencySR);
            distortSR.color = new Color(distortSR.color.r, distortSR.color.g, distortSR.color.b, transparencyDistort);
            rendererGO.gameObject.SetActive(true);

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
            rendererGO.gameObject.SetActive(false);
        }
    }
}