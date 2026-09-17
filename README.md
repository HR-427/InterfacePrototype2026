# InterfacePrototype2026

This repository contains the Unity 3D prototype developed for my undergraduate dissertation, which investigated the use of augmented reality (AR) interfaces projected onto a vehicle windscreen. The project explores how future in-car interfaces could be designed to improve the driving experience while minimising driver distraction.

DISSERTATION:
 - https://drive.google.com/file/d/19RCANeFNXH3u0iXNVoOzOmdU-qEImdUc/view?usp=sharing

OVERVIEW

The prototype was developed in Unity 3D to compare three different interface layouts:

- Screen Fixed – Interface elements remain fixed to the driver's view.
- World Fixed – Interface elements are anchored within the driving environment.
- Hybrid – A combination of both screen-fixed and world-fixed elements.

Extensive research into existing automotive interfaces and emerging AR Head-Up Display (HUD) technologies informed the design of this prototype. Automotive manufacturers such as BMW and Hyundai Mobis are actively researching and developing windscreen AR systems, helping shape the direction of future vehicle interfaces.

REFERENCES:
- BMW AR Head-Up Display:
  https://www.theverge.com/2025/1/7/24335460/bmw-ces-2025-idrive-heads-up-display-ar

- Hyundai Mobis AR HUD:
  https://www.prnewswire.com/news-releases/hyundai-mobis-enters-windshield-head-up-display-market-supplying-in-genesis-suv-gv80-301014183.html

PROJECT STRUCTURE

The project contains three Unity scenes:

- Screen Fixed
- World Fixed
- Hybrid

Each scene represents a different interface layout while maintaining the same driving task, allowing the layouts to be compared under consistent conditions.

CONTROLS

A / D      - Steer the vehicle left and right
Space      - Register a detected hazard
E          - Display the final performance results after completing the course

RUNNING THE PROTOTYPE

1. Open the project in Unity.
2. Select one of the three scenes.
3. Press Play to begin.
4. The vehicle moves automatically along the course.
5. Use A and D to steer and press Space whenever a hazard is detected.
6. Each trial lasts approximately two minutes.
7. At the end of the course, the vehicle stops automatically. Press E to display the final performance statistics in the Unity Console.

