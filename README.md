# ObjectClone


ObjectClone is a simple C# deep cloning library that supports any object or collection of objects

### Usage

For simple objects

```csharp
using ObjectClone;
.
.
.
var obj = //create your object
var clone = obj.DeepClone();
```

For collections

```csharp
using ObjectClone;
.
.
.
List<string> strings = new List<string> {}; //init your list
var clone = strings.DeepCloneList<string, List<string>>();
```

### How you can help

 If you discover issues make sure to write them up or fix them up yourself! Before issueing a pull request please make sure all the unit tests are passing, and write some unit tests for the case that you fixed/functionality you implemented
 
 ##### Current version: 0.1
