#include "context.h"

// System Headers
#include <iostream>

// Library Headers
#include "glew/GL/glew.h"
#include "glfw/glfw3.h"

// Error callback called by OpenGL whenever a problem occurs (vendor-dependent)
void APIENTRY errorCallback(GLenum source, GLenum type, GLuint id, GLenum severity,
	GLsizei length, const GLchar *message,
	const void *userParam)
{
	std::cerr << message << std::endl;
}

bool context::init(int width, int height, const char * title)
{
	glfwInit();

	window = glfwCreateWindow(width, height, title, nullptr, nullptr);

	glfwMakeContextCurrent(window);

	glewInit();

	#ifdef _DEBUG
	glEnable(GL_DEBUG_OUTPUT);
	glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);

	glDebugMessageCallback(errorCallback, 0);
	glDebugMessageControl(GL_DONT_CARE, GL_DONT_CARE, GL_DONT_CARE, 0, 0, true);
	#endif // _DEBUG

	std::cout << "OpenGL Version: " << (const char *)glGetString(GL_VERSION) << "\n";
	std::cout << "Renderer: " << (const char *)glGetString(GL_RENDERER) << "\n";
	std::cout << "Vendor :" << (const char *)glGetString(GL_VENDOR) << "\n";
	std::cout << "GLSL: " << (const char *)glGetString(GL_SHADING_LANGUAGE_VERSION) << "\n";

	glClearColor(0.25f, 0.25f, 0.25f, 1.0f);

	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDepthFunc(GL_LEQUAL);

	return true;
}

void context::tick()
{
	glfwPollEvents();
	glfwSwapBuffers(window);
}

void context::term()
{
	glfwDestroyWindow(window);
	glfwTerminate();

	window = nullptr;
}

bool context::shouldClose()const
{
	return glfwWindowShouldClose(window);
}

void context::clear()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}

// Get the current real-world time
float time::systemTime() const
{
	return 0.0f;
}
// Time between frames
float time::deltaTime() const
{
	return lastDeltaTime;
}
// Reset time to zero again
void time::resetTime()
{
	totalTime = 0.0f;
}
// Set time to a new value
void time::setTime(float newTime)
{
	totalTime = newTime;
}
