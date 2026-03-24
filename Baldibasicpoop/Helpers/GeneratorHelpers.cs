using HarmonyLib;
using MTM101BaldAPI;
using UnityEngine;

namespace Baldibasicpoop.Helpers
{
    public class GeneratorHelpers
    {
        /// <summary>
        /// Adds a weighted NPC to the Level.
        /// </summary>
        public void AddNPCToLevel(SceneObject scene, NPC npc, int Weight)
        {
            scene.potentialNPCs.Add(
            new WeightedNPC()
            {
                selection = npc,
                weight = Weight
            });
        }

        /// <summary>
        /// Adds a forced NPC's to the Level.
        /// </summary>
        public void AddNPCToLevel(SceneObject scene, NPC npc)
        {
            scene.forcedNpcs = scene.forcedNpcs.AddToArray(npc);
            scene.additionalNPCs = Mathf.Max(scene.additionalNPCs - 1, 0);
        }

        /// <summary>
        /// Adds a List of forced NPC's to the Level.
        /// </summary>
        public void AddNPCToLevel(SceneObject scene, NPC[] npcs)
        {
            scene.forcedNpcs = scene.forcedNpcs.AddRangeToArray(npcs);
            scene.additionalNPCs = Mathf.Max(scene.additionalNPCs - npcs.Length, 0);
        }

        /// <summary>
        /// Adds a weighted Item to Johnny's Shop.
        /// </summary>
        public void AddItemToShop(SceneObject scene, ItemObject item, int Weight)
        {
            scene.shopItems = scene.shopItems.AddToArray(
                new WeightedItemObject()
                {
                    selection = item,
                    weight = Weight
                });
        }

        /// <summary>
        /// Adds a weighted Item to the Level.
        /// </summary>
        public void AddItemToLevel(CustomLevelObject obj, ItemObject item, int Weight)
        {
            obj.potentialItems = obj.potentialItems.AddToArray(
                new WeightedItemObject()
                {
                    selection = item,
                    weight = Weight
                });
        }

        /// <summary>
        /// Adds a weighted Poster to the Level.
        /// </summary>
        public void AddPosterToLevel(CustomLevelObject obj, PosterObject Poster, int Weight)
        {
            obj.posters = obj.posters.AddToArray(
                new WeightedPosterObject()
                {
                    selection = Poster,
                    weight = Weight
                });
        }

        /// <summary>
        /// Adds a Room Group to the Level.
        /// </summary>
        public void AddRoomGroupToLevel(CustomLevelObject obj, RoomGroup Group)
        {
            obj.roomGroup = obj.roomGroup.AddToArray(Group);
        }

        /// <summary>
        /// Adds a weighted Structure With Parameters to the Level.
        /// </summary>
        public void AddStructureWithParametersToLevel(CustomLevelObject obj, StructureBuilder structure, IntVector2[] minmax, int Weight)
        {
            obj.potentialStructures = obj.potentialStructures.AddToArray(new WeightedStructureWithParameters()
            {
                selection = new StructureWithParameters()
                {
                    parameters = new StructureParameters()
                    {
                        minMax = minmax
                    },
                    prefab = structure
                },
                weight = Weight
            });
        }

        /// <summary>
        /// Adds a Structure With Parameters to the Level.
        /// </summary>
        public void AddStructureWithParametersToLevel(CustomLevelObject obj, StructureBuilder structure, IntVector2[] minmax)
        {
            obj.forcedStructures = obj.forcedStructures.AddToArray(new StructureWithParameters()
            {
                parameters = new StructureParameters()
                {
                    minMax = minmax
                },
                prefab = structure
            });
        }

        public void AddEventToLevel(CustomLevelObject obj, RandomEvent RandomEvent, int Weight)
        {
            obj.randomEvents.Add(new WeightedRandomEvent()
            {
                selection = RandomEvent,
                weight = Weight
            });
        }
    }
}
