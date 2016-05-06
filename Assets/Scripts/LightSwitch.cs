using UnityEngine;
using Stimuli;

public enum LightState {
    Off, On
}

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(VisualStimulus))]
public class LightSwitch : MonoBehaviour {
    [SerializeField]
    private LightState lightState = LightState.Off;
    [SerializeField]
    private Color lightColor = new Color(1, 1, 0.5f, 1);

    private new Light light;
    private new MeshRenderer renderer;
    private VisualStimulus visualStimulus;

    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
        renderer = GetComponent<MeshRenderer>();
        visualStimulus = GetComponent<VisualStimulus>();

        light.color = lightColor;
        if (lightState == LightState.Off) {
            TurnOff();
        } else {
            TurnOn();
        }
	}
	
    void OnMouseDown() {
        ToggleSwitch();
    }

    [Ability]
    public void TurnOn() {
        light.intensity = 8;
        renderer.material.color = lightColor;
        visualStimulus.data.brightness = 100;
    }

    [Ability]
    public void TurnOff() {
        light.intensity = 0;
        renderer.material.color = new Color(0.5f, 0.5f, 0.5f, 1);
        visualStimulus.data.brightness = 0;
    }

    [Ability]
    public void ToggleSwitch() {
        if (lightState == LightState.Off) {
            TurnOn();
            lightState = LightState.On;
        } else {
            TurnOff();
            lightState = LightState.Off;
        }
    }
}