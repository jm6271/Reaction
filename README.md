# Reaction

Reaction is a simple desktop app that measures your visual reaction time.
When the screen changes from red to green, click anywhere in the window as quickly as you can. The app records how long it takes for you to respond.

Reaction time tests are commonly used in psychology, gaming, and sports training to measure how quickly a person can respond to a visual stimulus.

## How It Works

The test follows a simple sequence:

1. Click the window to start.
2. The background turns red, indicating that the test is about to begin.
3. After a random delay, the background changes to **green**.
4. Click as quickly as possible.
5. The app displays the time between the color change and your click.

The delay before the green signal is randomized so that the moment of the signal cannot be predicted.

## Typical Reaction Times

Human reaction times vary depending on the type of stimulus and the individual.

| Category                  | Typical Visual Reaction Time |
| ------------------------- | ---------------------------- |
| Average adult             | 220–250 ms                   |
| Gamers / athletes	        | 180–220 ms                   |
| Elite competitive players | 150–190 ms                   |


Reaction time naturally fluctuates from attempt to attempt, so it’s best to run several trials and look at your average or fastest result.

## Why Use a Desktop App?

Many reaction tests are available as web apps, but a native desktop application has a few advantages when measuring timing:

- **Lower input latency** – Mouse events reach the application with fewer layers than in a browser.
- **High-resolution timing** – The app uses a high-precision system timer to measure elapsed time.
- **Less background overhead** – Web pages may be affected by browser processes, extensions, background tabs, and scripts.
- **More consistent timing** – A dedicated application avoids the browser event loop and JavaScript scheduling delays.

These factors help reduce timing jitter and produce more consistent measurements.

## Accuracy Notes

Even with a native application, several factors limit the precision of any software-based reaction test:

- **Monitor refresh rate** – On a 60 Hz display, frames update every ~16.7 ms.
- **Input hardware latency** – Mouse electronics introduce small delays.
- **Human variability** – Reaction time varies slightly each attempt.

Because of these limits, the result should be viewed as an **approximate measurement**, but it is still useful for comparing performance between trials.

## Installing

1. Download the latest installer from the **GitHub Releases** page.
2. Run the installer.
3. Launch the application.

Currently **Windows is the only supported platform**.

## Usage

1. Click anywhere in the window to begin.
2. Wait for the background to change from red to green.
3. Click again as quickly as you can.
4. Your reaction time will be displayed.

If you click before the green signal appears, the test ends and you will need to try again.

## Tips
- Focus on the center of the screen.
- Keep your hand relaxed and ready to click.
- Run multiple tests and compare your average reaction time.

## License

MIT License.
