# Min/max Range Attribute
![openupm](https://img.shields.io/npm/v/com.lazysquirrellabs.minmaxrangeattribute?label=openupm&registry_uri=https://package.openupm.com) 

A bounded (i.e., with a minimum and maximum) range attribute for Unity's `Vector2` and `Vector2Int` fields that draws fields as min/max range sliders, easing the definition of bounded ranges on the inspector.

![Five examples of attribute usage on the Unity inspector.](https://minmax.matheusamazonas.net/assets/images/header.gif)

## Features
- Intuitive, compact inspector representation.
- Support for Unity's `Vector2` and `Vector2Int` types.
- Easy to use: just add the attribute to a serialized field of a supported type.
- Custom floating-point decimal places (0 to 3).
- Error prevention. Unlike using separate fields for minimum and maximum, value integrity is guaranteed (e.g., the minimum will never be greater than the maximum) by the inspector.
- Uses built-in Unity's  [`EditorGUILayout.MinMaxSlider`](https://docs.unity3d.com/ScriptReference/EditorGUILayout.MinMaxSlider.html) under the hood.


## Usage
To use the attribute, simply add it to a serialized field of a supported type (`Vector2` or `Vector2Int`). Its inspector representation will be a [`MinMaxSlider`](https://docs.unity3d.com/ScriptReference/EditorGUILayout.MinMaxSlider.html), a slider than can be used to represent a range within minimum and maximum limits. The left handle controls the vector's `x` component and the right handle controls the vector's 

### Vector2 usage
When using the attribute on a field of type `Vector2`, its constructor takes 3 arguments:
- `minLimit`: the minimum possible value (lower bound).
- `maxLimit`: the maximum possible value (upper bound).
- `decimals`: how many decimal places the inspector should display. Default is 1 and values must be in the [0, 3] range.

For example, the field below has `minLimit` equal to 0, `maxLimit` equal to 10 and `decimals` equal to 3:
```csharp
[MinMaxRange(0f, 10f, 3)]
[SerializeField] private Vector2 _optimalSpeed = new (3.141f, 5.789f);
```

And its inspector representation is:
![](https://minmax.matheusamazonas.net/assets/images/usage/vector_3.png)

If `decimals` is 2 (`MinMaxRange(0f, 10f, 2)`):
![](https://minmax.matheusamazonas.net/assets/images/usage/vector_2.png)

The default value of `decimals` is 1, so we might as well omit the parameter if we would like to display only 1 decimal place:
```csharp
[MinMaxRange(0f, 10f)]
[SerializeField] private Vector2 _optimalSpeed = new (3.141f, 5.789f);
```

Which will be displayed as:
![](https://minmax.matheusamazonas.net/assets/images/usage/vector_1.png)

Keep in mind that the `decimals` parameter only controls how the value labels will be displayed on the inspector. It doesn't control the values' precision.

### Vector2Int usage
When using the attribute on a field of type `Vector2Int`, its constructor takes 2 arguments, similar to `RangeAttribute`'s parameters:
- `minLimit`: the minimum possible value (lower bound).
- `maxLimit`: the maximum possible value (upper bound).

For example, the field below has `minLimit` equal to 0 and `maxLimit` equal to 10:
```csharp
[MinMaxRange(0, 10)]
[SerializeField] private Vector2Int _rewardRange = new(2, 4);
```
And its inspector representation is:
![](https://minmax.matheusamazonas.net/assets/images/usage/vector_int.png)

## Importing
The first step is to import the library into your Unity project. There are two ways to do so: via the Package Manager using a git URL, and via OpenUPM.

### Import using a git URL
This approach uses Unity's Package Manager to add the attribute to your project using the repo's git URL. To do so, navigate to `Window > Package Manager` in Unity. Then click on the `+` and select "Add package from git URL":

![](https://minmax.matheusamazonas.net/assets/images/upm_adding.png)

Next, enter the following in the "URL" input field to install the latest version of the attribute:
```
https://github.com/lazysquirrellabs/min_max_range_attribute.git?path=Assets/Libraries/MinMaxRangeAttribute#latest
```
Finally, click on the "Add" button. The importing process should start automatically. Once it's done, the attribute can be used in your project. 

### Import with OpenUPM
Min/max Range Attribute is available as a package on [OpenUPM](https://openupm.com/packages/com.lazysquirrellabs.minmaxrangeattribute/). To import it into your project via the command line, run the following command:
```
openupm add com.lazysquirrellabs.minmaxrangeattribute
```
Once the importing process is complete, the attribute can be used in your project. 

## Compatibility and dependencies
The Min/max Range Attribute requires Unity 2021.3.X or above, its target API compatibility level is .NET Standard 2.1, and it does not depend on any other packages.

## Contributing
If you would like to report e bug, please create an [issue](https://github.com/lazysquirrellabs/min_max_range_attribute/issues). If you would like to contribute with bug fixing or small improvements, please open a Pull Request. If you would like to contribute with a new feature,  [contact the developer](https://matheusamazonas.net/contact.html).  

## Getting help
Use the [issues page](https://github.com/lazysquirrellabs/min_max_range_attribute/issues) if there's a problem with your setup, if something isn't working as expected, or if you would like to ask questions about the tool and its usage.

## License
Min/max Range Attribute is distributed under the terms of the MIT license. For more information, check the [LICENSE](https://github.com/lazysquirrellabs/min_max_range_attribute/blob/main/LICENSE) file in this repository.