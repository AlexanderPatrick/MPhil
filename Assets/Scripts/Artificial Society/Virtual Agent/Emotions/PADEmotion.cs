using System;
using UnityEngine;

namespace Emotions {
    [Serializable]
    public struct PADEmotion {

        public float valence;
        public float arousal;
        public float dominance;


        //private float pleasure;
        //private float arousal;
        //private float dominance;

        public float Valence {
            get { return valence; }
            set {
                valence = value;
                // if (pleasure < -1) pleasure = -1;
                // if (pleasure > 1) pleasure = 1;
            }
        }

        public float Arousal {
            get { return arousal; }
            set {
                arousal = value;
                // if (arousal < 0) arousal = 0;
                // if (arousal > 1) arousal = 1;
            }
        }

        public float Dominance {
            get { return dominance; }
            set {
                dominance = value;
                // if (dominance < -1) dominance = -1;
                // if (dominance > 1) dominance = 1;
            }
        }

        public Color Colour {
            get {
                if (valence > 0) {
                    return Color.Lerp(Color.white, Color.yellow, valence);
                } else if (valence < -0.5f && dominance < 0) {
                    return Color.Lerp(Color.white, Color.blue, (-valence - 0.5f) / 0.5f);
                } else if (valence < -0.5f && dominance > 0) {
                    return Color.Lerp(Color.white, Color.red, (-valence - 0.5f) / 0.5f);
                } else if (valence > -0.5f && dominance > 0) {
                    return Color.Lerp(Color.white, Color.green, -valence / 0.5f);
                } else if (valence > -0.5f && dominance < 0) {
                    return Color.Lerp(Color.white, Color.magenta, -valence / 0.5f);
                } else {
                    return Color.white;
                }
            }
        } 

        // Copy Constructor
        public PADEmotion(PADEmotion copy) {
            valence = copy.valence;
            arousal = copy.arousal;
            dominance = copy.dominance;
        }

        public PADEmotion(float p, float a, float d) {
            valence = p;
            arousal = a;
            dominance = d;
        }

        public void Reset() {
            valence = 0;
            arousal = 0;
            dominance = 0;
        }

        public void Decay(float time, float relativeStrength) {
            float decayFactor = Mathf.Exp(-time / relativeStrength);
            valence *= decayFactor;
            arousal *= decayFactor;
            dominance *= decayFactor;
        }

        public override string ToString() {
            return "(" + valence + "," + arousal + "," + dominance + ")";
            // return base.ToString() + ": (" + pleasure + "," + arousal + "," + dominance + ")";
        }

        public static PADEmotion operator +(PADEmotion emotion1, PADEmotion emotion2) {
            return new PADEmotion(emotion1.valence + emotion2.valence, emotion1.arousal + emotion2.arousal, emotion1.dominance + emotion2.dominance);
        }

        public static PADEmotion operator -(PADEmotion emotion1, PADEmotion emotion2) {
            return new PADEmotion(emotion1.valence - emotion2.valence, emotion1.arousal - emotion2.arousal, emotion1.dominance - emotion2.dominance);
        }

        public static PADEmotion operator *(PADEmotion emotion, float multiplier) {
            return new PADEmotion(emotion.valence * multiplier, emotion.arousal * multiplier, emotion.dominance * multiplier);
        }

        public static PADEmotion operator /(PADEmotion emotion, float divisor) {
            return new PADEmotion(emotion.valence / divisor, emotion.arousal / divisor, emotion.dominance / divisor);
        }
    }
}