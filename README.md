

# Teacher Computer Retrieval Tool

## Overview

The **Teacher Computer Retrieval Tool** is a Windows Forms application built in C# to analyze delivery routes for computers between schools. Users can input a set of routes (e.g., "AB5" for A to B, 5 miles) and query the system for specific route distances, trip counts under various constraints, and the shortest route, with all results including detailed paths. This project is inspired by the classic "Trains" problem, adapted for a school context, and includes a modern UI and comprehensive unit tests.

### Features
- **Route Distance Calculation**: Computes the total distance for a specific route (e.g., "ABC" → 9 miles).
- **Trips with Max Stops**: Counts trips between two schools with a maximum number of stops, listing all paths (e.g., C to C, max 3 stops → 2 trips: C-D-C, C-E-B-C).
- **Trips with Exact Stops**: Counts trips with an exact number of stops, with paths (e.g., A to C, 4 stops → 3 trips: A-B-C-D-C, A-D-C-D-C, A-D-E-B-C).
- **Shortest Route**: Finds the shortest distance and path between two schools (e.g., A to C → 9 miles, A-B-C).
- **Trips with Distance Limit**: Counts trips under a distance threshold, with paths (e.g., C to C, <30 miles → 7 trips).
- **Modern UI**: Clean, intuitive interface with panels, tooltips, and multiline result display.
- **Unit Tests**: NUnit tests verify all functionality using the standard Trains problem input.

## Prerequisites

- **.NET Framework**: Version 4.8 or compatible (tested on 4.8).
- **Visual Studio**: 2019 or later recommended for building and running.
- **NUnit**: Required for running unit tests (installed via NuGet).

## Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/TeacherComputerRetrievalUI.git
   cd TeacherComputerRetrievalUI
   ```

2. **Open the Solution**:
   - Open `TeacherComputerRetrievalUI.sln` in Visual Studio.

3. **Restore NuGet Packages**:
   - Right-click the solution in Solution Explorer > `Restore NuGet Packages` (ensures NUnit is available).

4. **Build the Project**:
   - `Build` > `Build Solution` (or Ctrl+Shift+B).
   - Switch to `Release` configuration for a standalone executable (`Build` > `Configuration Manager`).

## Usage

### Running the Application
1. **Launch**:
   - Run in Visual Studio: Press `F5`.
   - Or, use the executable: `bin/Release/TeacherComputerRetrievalUI.exe`.

2. **Input Routes**:
   - In the "Routes" text box, enter routes in the format `AB5,BC4,...` (e.g., "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7").
   - Click "Build Map" to initialize the graph.

3. **Query the System**:
   - **Trip Distance**: Enter a route (e.g., "ABC") → "Result: 9".
   - **Max Stops**: Enter start/end (e.g., "C", "C") and max stops (e.g., "3") → "Result: 2 trips\nPaths: C-D-C, C-E-B-C".
   - **Exact Stops**: Enter start/end (e.g., "A", "C") and exact stops (e.g., "4") → "Result: 3 trips\nPaths: A-B-C-D-C, A-D-C-D-C, A-D-E-B-C".
   - **Shortest Route**: Enter start/end (e.g., "A", "C") → "Result: 9 miles\nPath: A-B-C".
   - **Distance Limit**: Enter start/end (e.g., "C", "C") and max distance (e.g., "30") → "Result: 7 trips\nPaths: C-D-C, C-E-B-C, ...".

### Running Unit Tests
1. **Open Test Explorer**:
   - `Test` > `Test Explorer` in Visual Studio.

2. **Run Tests**:
   - Click "Run All" (or `Ctrl+R, A`).
   - Tests verify functionality with the input "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7".

## Project Structure

- **`TeacherComputerRetrieval/`**:
  - `Graph.cs`: Core graph logic (parsing, distance calculation, trip counting, shortest path).
- **`TeacherComputerRetrievalUI/`**:
  - `Form1.cs`, `Form1.Designer.cs`: Modern UI implementation.
  - `Program.cs`: Application entry point.
- **`TeacherComputerRetrieval.Tests/`**:
  - `GraphTests.cs`: NUnit unit tests.

## Example Output

With input routes "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7":
- **Trip Distance (ABC)**: "Result: 9"
- **Max Stops (C to C, 3)**: "Result: 2 trips\nPaths: C-D-C, C-E-B-C"
- **Shortest Route (A to C)**: "Result: 9 miles\nPath: A-B-C"

## Testing

The project includes comprehensive unit tests in `GraphTests.cs`:
- Validates route distances (e.g., "ABC" → 9).
- Checks trip counts and paths (e.g., C to C, max 3 → 2 trips).
- Verifies shortest routes (e.g., A to C → 9, A-B-C).
- Tests edge cases (e.g., invalid routes, empty input).

Run tests to ensure all pass before submission.

## Contributing

This is a completed project for submission, but feel free to fork and enhance it:
- Add more UI features (e.g., reset button, path visualization).
- Optimize performance for larger graphs.

## Author

- **[Your Name]**  
- Created: March 07, 2025

## License

This project is for educational purposes and not licensed for commercial use.

---

### Instructions to Use
1. **Create the File**:
   - In your project root folder, create a file named `README.md`.
   - Copy-paste the content above into it.

2. **Customize**:
   - Replace `yourusername` in the `git clone` URL with your GitHub username.
   - Add your actual name under "Author".
   - Adjust the date if needed.

3. **Push to GitHub**:
   - Initialize Git if not already done:
     ```bash
     git init
     git add README.md
     git commit -m "Add README for submission"
     ```
   - Create a GitHub repo (e.g., "TeacherComputerRetrievalUI"), then:
     ```bash
     git remote add origin https://github.com/yourusername/TeacherComputerRetrievalUI.git
     git push -u origin main
     ```
   - Verify the README renders correctly on GitHub.

4. **Submit**:
   - Share the GitHub URL (e.g., `https://github.com/yourusername/TeacherComputerRetrievalUI`) with your instructor.

### Tips
- **Screenshots**: Optionally, add a screenshot of the UI in action (e.g., showing "Result: 9 miles\nPath: A-B-C"). Upload the image to the repo and link it:
  ```markdown
  ![UI Screenshot](screenshot.png)
  ```
- **Badges**: Add GitHub badges for fun (e.g., build status if you set up CI):
  ```markdown
  [![.NET](https://img.shields.io/badge/.NET-Framework%204.8-blue)](https://dotnet.microsoft.com)
  ```

