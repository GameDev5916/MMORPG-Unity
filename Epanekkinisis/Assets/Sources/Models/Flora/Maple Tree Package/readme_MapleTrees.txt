Working with the Maple Tree Package
--------------------------------------------

1. Optimized Textures and Materials
--------------------------------------------
Please note that all tree prefabs share the same materials and textures in order to save texture memory and texture load at runtime.
Usually unity automaticly creates materials and textures for each tree.
I have just deleted all those texture folders except for one, then assigned the materials "Optimized Bark Metrial" and "Optimized Leaf Material" both located in the Prefab "xyz Master" to each tree by dragging them from the project tab to the proper slot of each tree in the inspector tab.

Changing any parameter on any tree will unity force to recalculate the textures so the additional folders will be recreated. In this case just delete the new folders and reassign the original materials like described above.

Those materials use the textures you find in the folder "xyz Master_Textures". Please note the special texture settings of those textures: High Aniso Level [9] on the diffuse texture and kaiser-mipmapping.

Next to that this package comes with high resolution textures for bark and leaves [diffuse only] at a resolution of 2048x1024px which usually is not supported by the tree creator.
You will find this texture in the the folder "Maple_old_Master_Textures" and it is called "diffuse_highres". i manually created this texture using photoshop and manually assigned it to both "Maple_old_Master" --> "Optimized Bark material" and "Optimized Leave Material" so it gets used on all trees as they all share the same material.