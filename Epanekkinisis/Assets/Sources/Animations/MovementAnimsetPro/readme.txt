Movement Animset Pro v.1.25
--------------------


This is a complete set of motion capture animations, to build a seamless third person perspective character movement for your game. The animations are universal, so you can use them for any setting you like - SciFi, Fantasy etc.

--------------------
V1.25 changes:
--------------------
- Converted all animations and characters to MECANIM Humanoid rig
--------------------


--------------------
V1.2 changes:
--------------------
- added fistfighting animations
- added death and knockdown animations
--------------------


--------------------
V1.1 changes:
--------------------
- added strafing and walking back animations
- tweaked WalkFwdLoop and all transitions to even the step lenght
--------------------


It consists of over 120 animations from which you can make game mechanics for:

- Standing
- Walking
- Running
- Crouching / Sneaking
- Jumping
- Falling
- Interactions (button pushing, picking up objects etc.)
- Fist fighting
- Deaths and knockdowns

It also contains:
- Skinned model of a modern day soldier (26,536 tris, 13,813 verts)
- Skinned model of a dummy, to preview animations

--------------------

Animations are cut to ready-to-use clips. They have an animated Root bone, so you can use Root Motion and move the character around just with animations. All the transition between standing, walking, running and crouching are included. Animations use Unity's Humanoid rig, but they can be easly converted to Generic rig, with 2 mouse clicks. If you wish to have all those animations in place (with no Root Motion), just uncheck "Enable Root Motion" in your Animator Prefab in the Scene or delete the animation from Root bone in animations.

The skeleton that animations are baked on to is a standard, Motionbuilder compatible skeleton. It has Motionbuilder hierarchy and naming convention. Hips, Neck and Head have Z axes pointing forward, so you can use Look At constraints etc.


--------------------
List of animations
--------------------

Idle

TurnRt90_Loop
TurnLt90_Loop
TurnRt180
TurnLt180

WalkFwdLoop
WalkFwdStart
WalkFwdStart180_R
WalkFwdStart180_L
WalkFwdStart90_L
WalkFwdStart90_R
WalkFwdStop_LU
WalkFwdStop_RU
WalkFwdLoop_LeanR
WalkArchLoop_R
WalkFwdLoop_LeanL
WalkArchLoop_L

RunFwdLoop
SprintFwdLoop
RunFwdStart
RunFwdStart180_R
RunFwdStart180_L
RunFwdStart90_R
RunFwdStart90_L
RunFwdStop_RU
RunFwdStop_LU
RunFwdTurn180_R_LU
RunFwdTurn180_R_RU
RunFwdTurn180_L_RU
RunFwdTurn180_L_LU
RunArchLoop_L
RunFwdLoop_LeanL
RunArchLoop_R
RunFwdLoop_LeanR

Jump_place_ALL
Jump_place_ALL_short
Jump_walk_ru_ALL
Jump_walk_lu_ALL
Jump_run_ru_ALL
Jump_run_lu_ALL

JumpIdleStart
JumpIdleLand
JumpIdleLand2Walk
JumpIdleLandHard

JumpWalkStart_RU
JumpWalk_RU_Land
JumpWalk_RU_Land2Walk
JumpWalkStart_LU
JumpWalk_LU_Land
JumpWalk_LU_Land2Walk

JumpRunStart_RU
JumpRun_RU_Land
JumpRun_RU_Land2Run
JumpRunStart_LU
JumpRun_LU_Land
JumpRun_LU_Land2Run

FallingLoop
FallingLoop_RootMotion

Idle2Crouch
Crouch_Idle
Crouch2Idle
Crouch_WalkFwdLoop
Crouch_WalkFwdStart
Crouch_WalkFwdStop_LU
Crouch_WalkFwdStop_RU
Crouch_WalkFwdStart180_R
Crouch_WalkFwdStart180_L
Crouch_WalkFwdStart90_R
Crouch_WalkFwdStart90_L

ButtonPush_RH
ButtonPush_RH_90
ButtonPush_LH
ButtonPush_LH_90

KeypadUse_RH
KeypadUse_RH_90
KeypadUse_LH
KeypadUse_LH_90

PickUp_RH
PickUp_RH_90
PickUp_LH
PickUp_LH_90

PullLever_RH
PullLever_RH_90
PullLever_LH
PullLever_LH_90

DontKnow

ThrowAway_RH
ThrowAway_LH

WalkThroughDoor_RH
WalkThroughDoor_LH

--------------------
Update 1.1: Additional motions
--------------------

WalkFwdStart135_R
WalkFwdStart135_L
RunFwdStart135_R
RunFwdStart135_L

WalkBwdStart
WalkBwdLoop
WalkBwdStop_RU
WalkBwdStop_LU
StrafeRightStart
StrafeRightLoop
StrafeRightStop_LU
StrafeRightStop_RU
StrafeLeftStart
StrafeLeftLoop
StrafeLeftStop_RU
StrafeLeftStop_LU

--------------------
Update 1.2: Fighting and deaths
--------------------

Idle2Fists
Fists_Idle
Fists2Idle
Idle_Punch_Move_L
Fists_Punch_Move_L
Fists_Punch_Move_R
Fists_Punch_L
Fists_Punch_R
Fists_Kick_Front_Move_R
Fists_Kick_Front_L
Fists_Punch_Heavy2Idle
Fists_Hit_Left
Fists_Hit_Right
Idle_Hit_Strong_Left
Idle_Hit_Strong_Right
Idle_Knockdown_Front
Idle_Knockdown_Right
Idle_Knockdown_Left
Death_1
Death_2
--------------------

See AnimationDescriptions.pdf to get full description of each animation.
http://www.kubold.com/MovementAnimsetProExample/MovementAnimsetPro_ListOfAnimations_v1.2.html




--------------------
Created by Kubold
kuboldgames@gmail.com
http://www.kubold.com
https://www.facebook.com/kuboldgames