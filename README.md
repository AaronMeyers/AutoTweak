# AutoTweak

I made this to speed the process of tweaking variables in Unity with the new UI system introduced in Unity 4.6. AutoTweak automates the process of building a UI while prototyping using C#'s attributes.
So far, it only supports floats/sliders, but I'd like to add toggles (for bools) and buttons (for functions) and possibly some kind of UI to deal with Vectors.

Instructions to use:

1. Import the .unitypackage file into your project.
2. Drag the AutoTweak.prefab into your scene from the AutoTweak/Prefabs folder
3. After importing the AutoTweak namespace, decorate any float variable with the SliderAttribute. The two arguments define the slider's range.
4. Add the Tweakable component and drag your component you are tweaking into the target slot in the inspector.
5. That's it! AutoTweak will build a panel with sliders for any decorated variables. It works with multiple targets too!
```
using UnityEngine;
using AutoTweak;

public class MyClass : MonoBehaviour {
  [SliderAttribute(0.0f, 10.0f)]
  float myFloat = 0.0f;
}

```
