# kaijurising
A game development project for students looking to get to PAX AUS 2015

## Contributing

### File Names
- PascalCasing

**Example:**

>ThisIsPascalCasing

>thisIsNotPascalCasing

- Descriptive names, no ambiguity allowed
- Don't make the name too long, word limit of 3 words maximum

### Folder Structure
- If something can be encompassed inside of a folder, place it inside there
- If a required folder for a file category doesn't exist, create and place the file there.
- The only time a sub category folder can be made, is when there are at least 2 files which can fit in that sub category

### Folder Names
- PascalCasing
- Word limit of 2 words
- Cascading Naming (You name a folder based on it's responsibility within the folder)

**Example:**

> Network/Player/Classes/Warrior.cs

> Offline/Player/Classes/OfflineWarrior.cs

- Descriptive based on the items it will contain(It must be a word which can categorise the files it contains)
  - Art contains all files related to art (png's, materials, fbxs, etc)
  - Script contains all .cs files

### When these rules are broken, the following must occur:
- Folder Structure
  - Move files and folders to a more appropriate location
  - Report to Mouhamad if it hasn't been noticed
  - And only then commit, reporting inside the commit message what had been changed.
-Folder Names
  - Check what's inside of the folder
  - If folder structure is broken, refer to 'Folder Structure'
  - Otherwise rename it based on what's inside of it
- File Names
  - Check what the file is and rename it appropriately if you can
  - If you can't work out what it should be, speak to the person who committed it and have them correct the issue!
  - If you can correct the name, commit it and report it within the commit message.

### Coding Conventions of the Project

For class variable declaration, the following must be enforced:
- The names MUST BE A NOUN!
- The name of a variable cannot exceed 2 words.
- Array variables MUST BE A COLLECTIVE NOUN OR PLURAL
- public variables are at the top most of the list
- then protected
- then private

**Example:**

```
public int variable;
public Vector3 anotherVar;
protected float number;
private string word;
```

### Local variable declaration, the following must be enforced:
- All local variables to be used throughout a function MUST BE declared at the top of the function.
- All local variables MUST BE initialised as well.

**Example:**

```
private void Start()
{
  int var;
  float num;
  bool yes;
  string word;
            
  var = 0;
  float = 0;
  yes = false;
  word = "";
}
```

**or**

```
//THIS WAY IS PREFERRED
private void Start()
{
  int var = 0;
  float num = 0;
  bool yes = false;
  string word = "";
}
```

### Function declaration, the following must be enforced:
- Curly braces of a function must be on a new line
  - The only exception to this rule is with the MonoBehaviour functions.
- the scope of a function must be explicitly defined (can't just type void Start())
- All MonoBehaviour functions must be located at the top of the class
- All user defined functions must be located at the bottom of the class
- User defined functions must follow camelCasing convention, NOT PASCALCASING
- Functions must be a VERB
- Functions must be no longer than 3 Words total.
- Parameters and arguments of a function must be descriptive, no shorthand variable names.
- If a MonoBehaviour function is empty with nothing inside of it, it should not be inside the script file.
- A function can not exceed 50 lines of code.
  - If a function is found to have more than 50 lines of code, it will be commented out until rectified. The function should be split off into different functions.

**Example:**

```
public class Example : MonoBehaviour
{
  private void Start()
  {
            
  }
            
  private void Update()
  {
            
  }
            
  private void OnCollisionEnter(Collision col)
  {
  
  }
            
  private void checkKeys()
  {
  
  }
            
  private void move()
  {
  
  }
            
  public void jump()
  {
  
  }
}
```
