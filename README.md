# WeLeap

Our work explores the utilization of Leap Motion technology to design an experimental navigation system intended for virtual 3D environments. We integrate 5-DOF movement using hand gestures and devise a more intricate solution that demands greater levels of coordination.

https://github.com/FabioMiguel2000/Leap-Motion-3D-Navigation/assets/100025288/e61a77a7-7030-4301-96cc-4656e0c1036b

## User Interface

<p align="center">
   <img width="412" alt="user_interface_img" src="https://github.com/FabioMiguel2000/blob/main/img/user_interface.png">
</p>

## Architecture

<p align="center">
   <img width="412" alt="architecture_diagram" src="https://github.com/FabioMiguel2000/blob/main/img/architecture_diagram.png">
</p>

## Pre-requisites
- Leap Motion Controller (hardware)
- Ultraleap Hand Tracking Service - v5 (software)
- Unity Editor 2022.3.11f1.


## Navigation Control

1) **Navigation Mode**: In Navigation mode, the left hand manages the left/right and forward/backward translation through tilting movements: left/right for lateral motion and forward/backward for longitudinal movement. Additionally, it facilitates vertical translation by raising or lowering the hand. On the other hand, the right hand oversees camera rotation in accordance with its vector orientation. For instance, when the palm’s normal aligns perpendicularly to the screen, indicating an upward hand vector, the camera rotates in an upward direction. The translation and rotation speeds can be dynamically adjusted based on real-time changes and are directly proportional to the distance from the neutral position. For example, as the hand tilts forward, the forward translation accelerates, and as the x-coordinate of the hand vector increases, the camera’s upward rotation speed intensifies.
2) **Orbit Mode**: In Orbit mode, the functionality is limited to rotation and forward/backward translation. Consequently, the left hand exclusively manages forward and backward translation, while the right hand operates similarly to the navigation mode, but it orchestrates rotations around the orbit center instead of around the user.
3) **Mode Switching**: Toggling between navigation and orbit modes occurs through a pointing gesture. In navigation mode, the gesture triggers the creation of an indicator that continuously moves outward in the direction the camera faces until the gesture ends. Upon the gesture’s cessation, the mode switches to orbit, with the indicator serving as the new center for orbiting. When in orbit mode, using the pointing gesture followed by
its reversal (“unpointing”) removes the indicator and reverts the mode back to navigation.

## Acknowledgements
A special thanks to [Francisco Cerqueira](https://github.com/xico2001pt) and [Vasco Alves](https://github.com/Vasco52) for their efforts in developing the competing system [WiiFly](https://github.com/xico2001pt/wiifly-3d-navigation). The development of both systems was carried out in parallel, with the final goal of comparing the two approaches in a self-developed user testing and evaluation method.

## License 
This project is licensed under the Apache License, Version 2.0.

For the complete text of the Apache License, please refer to the [Apache License](https://github.com/FabioMiguel2000/Leap-Motion-3D-Navigation/blob/main/LICENSE).