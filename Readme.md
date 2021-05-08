# Modular Characters Basic Implementation

-   This includes an editor to create modular characters base which can be used for advanced features you might want to implement.

-   Tried to follow the in built "create ragdoll" window as much as possible.

-   Access the editor window through either:
    1. Right click your object and select Create Modular Character
    2. Or it is accesible from the top menu bar under "GameObject/Create Modular Character"

## Additional Features:

-   You can add additional slots.
-   When the Slot gets filled with correct values, the objects are auto parented to the assigned parents and aligned to the parent too.
-   Ability to lock the editor window on the current character
-   Ability to align the object in the new slot to its parent, alternatively you can choose to only align position or rotation.

## Temporary arrangements:

-   Using lists to store the slots in the base and it's easily debuggable as they are readily serialised. Which could be improved to using dictionaries and removing the field name from Slot class altogether.
