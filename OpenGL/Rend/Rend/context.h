#pragma once

class context 
{
	struct GLFWwindow * window;

public:
	bool init(int width, int height, const char * title);
	void tick();
	void term();
	void clear();

	bool shouldClose() const;
};

// A time-keeping class
class time
{
	private:
		// Time since the start of the program
		float totalTime;
		// Time at the end of the last frame
		float lastDeltaTime;
	
	public:
		// Time since the start of the program
		// float time() const;
		// Get the current real-world time
		float systemTime() const;
		// Time between frames
		float deltaTime() const;

		// Reset time to zero again
		void resetTime();
		// Set time to a new value
		void setTime(float newTime);
};