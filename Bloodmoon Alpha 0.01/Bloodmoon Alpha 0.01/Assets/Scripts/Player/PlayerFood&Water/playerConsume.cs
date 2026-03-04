from pynput.keyboard import Key, Controller
import time
import random

keyboard = Controller()

def hold_shift_loop():
    print("Script started. Press Ctrl+C in this terminal to stop.")
    try:
        while True:
            # Press and hold Left Shift
            keyboard.press(Key.shift_left)
            
            # Choose a random time between 3 and 4 seconds
            wait_time = random.uniform(3.0, 4.0)
            print(f"Holding Shift for {wait_time:.2f} seconds...")
            time.sleep(wait_time)
            
            # Release and immediately re-press happens in the next loop iteration
            keyboard.release(Key.shift_left)
            
            # Optional: tiny delay so the OS registers the "up/down" cycle
            time.sleep(0.05) 
            
    except KeyboardInterrupt:
        # Ensure Shift is released when you stop the script
        keyboard.release(Key.shift_left)
        print("\nScript stopped and Shift released.")

if __name__ == "__main__":
    # Give yourself 5 seconds to switch to your game/app window
    print("Starting in 5 seconds...")
    time.sleep(5)
    hold_shift_loop()