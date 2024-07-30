using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
namespace Plugins.Sirenix.Demos.Sample___RPG_Editor.Scripts.Editor
{
    //
    // This class is used by the RPGEditorWindow to render an overview of all characters using the TableList attribute.
    // All characters are Unity objects though, so they are rendered in the inspector as single Unity object field,
    // which is not exactly what we want in our table. We want to show the members of the unity object.
    //
    // So in order to render the members of the Unity object, we'll create a class that wraps the Unity object
    // and displays the relevant members through properties, which works with the TableList, attribute.
    //

    public class CharacterTable
    {
        [FormerlySerializedAs("allCharecters")]
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<CharacterWrapper> allCharacters;

        public Character.Character this[int index]
        {
            get { return allCharacters[index].Character; }
        }

        public CharacterTable(IEnumerable<Character.Character> characters)
        {
            allCharacters = characters.Select(x => new CharacterWrapper(x)).ToList();
        }

        private class CharacterWrapper
        {
            private Character.Character character; // Character is a ScriptableObject and would render a unity object
                                         // field if drawn in the inspector, which is not what we want.

            public Character.Character Character
            {
                get { return character; }
            }

            public CharacterWrapper(Character.Character character)
            {
                this.character = character;
            }

            [TableColumnWidth(50, false)]
            [ShowInInspector, PreviewField(45, ObjectFieldAlignment.Center)]
            public Texture Icon { get { return character.Icon; } set { character.Icon = value; EditorUtility.SetDirty(character); } }

            [TableColumnWidth(120)]
            [ShowInInspector]
            public string Name { get { return character.Name; } set { character.Name = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Shooting { get { return character.Skills.Shooting; } set { character.Skills.Shooting = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Melee { get { return character.Skills.Melee; } set { character.Skills.Melee = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Social { get { return character.Skills.Social; } set { character.Skills.Social = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Animals { get { return character.Skills.Animals; } set { character.Skills.Animals = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Medicine { get { return character.Skills.Medicine; } set { character.Skills.Medicine = value; EditorUtility.SetDirty(character); } }

            [ShowInInspector, ProgressBar(0, 100)]
            public float Crafting { get { return character.Skills.Crafting; } set { character.Skills.Crafting = value; EditorUtility.SetDirty(character); } }
        }
    }
}
#endif
