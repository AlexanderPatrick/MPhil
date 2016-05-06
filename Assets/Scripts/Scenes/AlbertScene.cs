using UnityEngine;
using System.Collections;

public class AlbertScene : AbstractScene {
    // Characters
    private GameObject albert;
    private GameObject mouse;
    private GameObject metalPole;
    private GameObject watson;

    public override void Setting() {
        albert = GameObject.Find("Albert");
        mouse = GameObject.Find("Rat");
        metalPole = GameObject.Find("Metal");
        watson = GameObject.Find("Watson");
        albert.transform.position = new Vector3(0, 0, 0);
        mouse.transform.position = new Vector3(-10, 0, 0);
        metalPole.transform.position = new Vector3(10, 0, 0);
        watson.transform.position = new Vector3(11, 0, 0);
    }

    public override IEnumerator Plot() {
        while (mouse.transform.position.x < -5) {
            mouse.transform.Translate(2 * Time.deltaTime, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        watson.GetComponent<Watson>().StrikeMetal(metalPole.GetComponent<Metal>());
        //yield return new WaitForSeconds(1);
        //watson.GetComponent<Watson>().StrikeMetal(metalPole.GetComponent<Metal>());
        //yield return new WaitForSeconds(1);
        //watson.GetComponent<Watson>().StrikeMetal(metalPole.GetComponent<Metal>());
        /*
        while (metalPole.transform.position.x > 5) {
            metalPole.transform.Translate(-2 * Time.deltaTime, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        while (metalPole.transform.position.x < 10) {
            metalPole.transform.Translate(2 * Time.deltaTime, 0, 0);
            yield return null;
        }
        */
        yield return new WaitForSeconds(3);
        while (mouse.transform.position.x > -10) {
            mouse.transform.Translate(-2 * Time.deltaTime, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        while (mouse.transform.position.x < -5) {
            mouse.transform.Translate(2 * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}