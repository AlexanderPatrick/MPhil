using System;

[Serializable]
public class EpisodicBuffer {
    public Episode episode;

    public EpisodicBuffer() {
        episode = new Episode();
    }
}
