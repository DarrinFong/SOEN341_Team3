#! /bin/sh

# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# Change this the name of your project. This will be the name of the final executables as well.
# pwd is soen341

project="Coding_Game"

echo "Attempting to build Coding_Game for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile soen341/unity.log \
  -projectPath soen341 \
  -buildWindowsPlayer "soen341/Build/windows/Coding_Game.exe" \
  -quit

echo "Attempting to build Coding_Game for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile soen341/unity.log \
  -projectPath soen341 \
  -buildOSXUniversalPlayer "soen341/Build/osx/Coding_Game.app" \
  -quit

echo "Attempting to build Coding_Game for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile soen341/unity.log \
  -projectPath soen341 \
  -buildLinuxUniversalPlayer "soen341/Build/linux/Coding_Game.exe" \
  -quit

echo 'Logs from build'
cat soen341/unity.log
