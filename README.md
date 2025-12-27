# 🛋️ FurnichAR: AR Furniture Placement

An Augmented Reality application that allows users to visualize and arrange virtual furniture in their real-world environment. Built with Unity AR Foundation for cross-platform AR experiences.

> [!NOTE]
> This project was developed for educational purposes as part of a computer graphics course. It demonstrates AR implementation, 3D interaction, and mobile deployment.

**Key Features**:
- ✅ **Real-time AR surface detection** using ARCore/ARKit
- ✅ **Furniture placement** with visual placement indicators
- ✅ **Interactive furniture manipulation** (move, rotate, delete)
- ✅ **Cross-platform** (Android/iOS compatible)
- ✅ **Optimized for mobile performance**

---

## 📖 Overview

This project creates an AR experience where users can select virtual furniture from a catalogue, place it in their physical space using AR surface detection, and interact with placed furniture through move, rotate, and delete functions. The application is designed with an intuitive UI and optimized for real-world AR performance.

---

## 🚀 Quick Start

### 1. Prerequisites
- Unity 2022.3.62f1 or later
- Android device with ARCore support (for Android builds)
- iOS device with ARKit support (for iOS builds)

### 2. Setup & Installation
1. Clone the repository:
```bash
git clone https://github.com/its-aleezA/FurnichAR.git
```
2. Open the project in Unity 2022.3.62f1
3. Install required packages via Package Manager:
   - AR Foundation 6.3.1
   - Google ARCore XR Plugin 6.3.1 (for Android)
   - Apple ARKit XR Plugin 6.3.1 (for iOS)
   - XR Plugin Management 4.5.3

### 3. Running in Unity Editor
1. Open the main scene: `Assets/Scenes/MainAR.unity`
2. Use XR Device Simulator for testing without hardware:
   - W/A/S/D: Movement
   - Mouse: Look around
   - Space: Toggle movement modes
3. Press Play to test AR simulation

### 4. Building to Mobile
#### Android:
```bash
1. File → Build Settings → Android → Switch Platform
2. Connect Android device via USB with USB debugging enabled
3. Install ARCore from Google Play Store
4. Build and Run
```

#### iOS:
```bash
1. File → Build Settings → iOS → Switch Platform
2. Open in Xcode
3. Build to iOS device with ARKit support
```

---

## 🛠️ Technical Details

### Unity Version & Packages
- **Unity Editor**: 2022.3.62f1
- **Core Packages**:
  - AR Foundation 6.3.1
  - Google ARCore XR Plugin 6.3.1
  - Apple ARKit XR Plugin 6.3.1
  - XR Plugin Management 4.5.3
  - XR Interaction Toolkit 3.3.0
  - Device Simulator 1.0.1

### Architecture
- **AR Session Management**: AR Foundation with XR Origin setup
- **Furniture Interaction**: Custom scripts for placement, movement, rotation
- **UI System**: Canvas-based interface with mode switching
- **3D Models**: Imported furniture prefabs with materials and colliders

### Key Scripts
- `ARTapToPlaceObject.cs` - Main furniture placement logic
- `MoveObject.cs` - Furniture movement with AR camera attachment
- `RotateObject.cs` - Slider-based furniture rotation
- `RemoveObject.cs` - Furniture deletion
- `ChangeMenu.cs` - UI mode management
- `Recolour.cs` - Visual feedback for selected objects

---

## 🎮 User Guide

### Basic Usage
1. **Launch the app** on your mobile device
2. **Scan your environment** by moving the camera slowly
3. **Select furniture** from the catalogue menu
4. **Place furniture** by tapping on detected surfaces
5. **Use interaction modes** to adjust placed furniture:
   - **Move**: Attach furniture to camera for repositioning
   - **Rotate**: Use slider to rotate selected furniture
   - **Delete**: Tap to remove unwanted furniture

### Controls
- **Touch**: Primary interaction (tap to place/select)
- **UI Buttons**: Mode switching and furniture selection
- **Slider**: Fine rotation control in rotate mode

---

## 📁 Project Structure

```
AR-Furniture-Placement/
├── Assets/
│   ├── Scenes/
│   │   └── MainAR.unity          # Main AR scene
│   ├── Scripts/
│   │   ├── ARTapToPlaceObject.cs # Core placement logic
│   │   ├── MoveObject.cs         # Furniture movement
│   │   ├── RotateObject.cs       # Furniture rotation
│   │   ├── RemoveObject.cs       # Furniture deletion
│   │   ├── ChangeMenu.cs         # UI management
│   │   └── Recolour.cs           # Visual feedback
│   ├── Prefabs/
│   │   ├── Furniture/            # 3D furniture models
│   │   └── UI/                   # UI components
│   └── Materials/                # Shaders and materials
├── Packages/                     # Unity package manifests
├── ProjectSettings/              # Unity project configuration
├── Blender Files/                # Blender file of a furniture test model
└── README.md                     # This file
```

---

## 📱 Demo

![Cover Image](./docs/cover.jpg)
![Having fun](./docs/funky.jpg)

---

## 🔧 Development Notes

### AR Implementation Challenges
1. **AR Foundation Migration**: Upgraded from AR Foundation 4.x to 6.x with API changes
2. **Cross-platform Compatibility**: Android (ARCore) and iOS (ARKit) support
3. **Performance Optimization**: Mobile-friendly shaders and texture compression

### Key Design Decisions
1. **XROrigin over ARSessionOrigin**: Used modern AR Foundation 6.x architecture
2. **Dual Input Support**: Both touch (mobile) and mouse (editor simulation)
3. **Visual Feedback**: Color changes for selected furniture states
4. **UI Mode System**: Separate interfaces for different interaction modes

---

## 📱 Platform Support

| Platform | Status | Requirements |
|----------|---------|--------------|
| **Android** | ✅ Fully Supported | Android 7.0+, ARCore compatible device |
| **iOS** | ✅ Fully Supported | iOS 11.0+, ARKit compatible device |
| **Unity Editor** | ✅ Simulation Mode | XR Device Simulator |

---

## 🤝 Team Members

- **[Aleeza Rizwan](https://github.com/its-aleezA)**
- **[Zahra Shehzad](https://github.com/zahrashehzad03)**
- **[Maheen Arshad](https://github.com/maheenarshad2)**

**Course**: EC-301 Computer Graphics  
**Institution**: National University of Science and Technology

---

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- [Project-Based Augmented Reality Course with Unity Engine and AR Foundation](https://youtu.be/FJAO6jDYljs?si=XDKeYsqD62ZeyHUU) by freeCodeCamp.org on YouTube

---

## 🔗 Related Resources

- [AR Foundation Documentation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@6.3/manual/index.html)
- [Google ARCore Developer Guide](https://developers.google.com/ar/develop)
- [Apple ARKit Documentation](https://developer.apple.com/augmented-reality/)
- [Reference Used](https://varlabs.comp.nus.edu.sg/unitylab/learn/core_3_new.html/)
---

> [!NOTE]
> This project was developed for educational purposes. Furniture models are for demonstration only.
