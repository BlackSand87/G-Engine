using System;
using System.Collections.Generic;
using System.Text;

namespace GEngine.game.hierarchy
{
    static class Hierarchy
    {

        public static List<Actor> loadedActors;

        public static void Init()
        {
            loadedActors = new List<Actor>();
        }

        public static bool loadActor(Actor actor, int atlasOffset = 0, bool isUsingTextureAtlas = false)
        {
            loadedActors.Add(actor);
            actor.LoadActor(atlasOffset,isUsingTextureAtlas);
            return true;
        }
    }
}
