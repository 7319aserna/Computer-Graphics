#include "context.h"
#include "render.h"

#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"

#include "glm/gtc/type_ptr.hpp"

#include <fstream>
#include <iostream>
#include <string>

using namespace std;

geometry makeGeometry(vertex * verts, size_t vertCount, unsigned * indices, size_t indexCount)
{
	// Create an instance of geometry
	geometry newGeo = {};
	newGeo.size = indexCount;
	// Generate buffers
	glGenVertexArrays(1, &newGeo.vao);	// Vertex Array Object
	glGenBuffers(1, &newGeo.vbo);		// Vertex Buffer Object
	glGenBuffers(1, &newGeo.ibo);		// Index Buffer Object

	// Bind buffers
	glBindVertexArray(newGeo.vao);
	glBindBuffer(GL_ARRAY_BUFFER, newGeo.vbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, newGeo.ibo);

	// populate buffers
	glBufferData(GL_ARRAY_BUFFER, vertCount * sizeof(vertex), verts, GL_STATIC_DRAW);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indexCount * sizeof(unsigned), indices, GL_STATIC_DRAW);

	// Describe vertex data
	glEnableVertexAttribArray(0);	// Position
	glVertexAttribPointer(0, 4, GL_FLOAT, GL_FALSE, sizeof(vertex), (void*)0);

	glEnableVertexAttribArray(1);	// Normals
	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(vertex), (void*)16);

	glEnableVertexAttribArray(2);	// UVs
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, sizeof(vertex), (void*)32);

	// Unbind buffers (in a SPECIFIC order)
	glBindVertexArray(0);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	// Return the geometry
	return newGeo;
}

void freeGeometry(geometry &geo)
{
	glDeleteBuffers(1, &geo.vbo);
	glDeleteBuffers(1, &geo.ibo);
	glDeleteVertexArrays(1, &geo.vao);

	geo = {};
}

string loadShader(const char * file) {
	ifstream myFile;

	myFile.open(file, ios::in | ios::app);

	const char * basicConstChar;
	string basicString;
	// This would hold in each line of text
	string getlineString;

	if (myFile.is_open()) {
		while (getline (myFile, basicString))
		{
			cout << basicString << endl;
			getlineString += basicString + "\n";
		}
		myFile.close();
	}
	else { cout << "Error: File could not open" << endl; }

	basicConstChar = getlineString.c_str();
	std::cout << basicConstChar << std::endl;
	return basicConstChar;
}

shader makeShader(const char * vertSource, const char * fragSource)
{
	// Make the shader object
	shader newShad = {};
	newShad.program = glCreateProgram();

	// Create the shaders
	GLuint vert = glCreateShader(GL_VERTEX_SHADER);
	GLuint frag = glCreateShader(GL_FRAGMENT_SHADER);

	// Compile the shaders
	glShaderSource(vert, 1, &vertSource, 0);
	glShaderSource(frag, 1, &fragSource, 0);

	glCompileShader(vert);

	// Error checking for vertex shader
	GLint vertex_Compiled;
	glGetShaderiv(vert, GL_COMPILE_STATUS, &vertex_Compiled);
	if (vertex_Compiled != GL_TRUE) {
		GLsizei log_Length = 0;
		GLchar message[1024];
		glGetShaderInfoLog(vert, 1024, &log_Length, message);
		cout << message << endl;
	}

	glCompileShader(frag);

	// Error checking for frag shader
	GLint frag_Compiled;
	glGetShaderiv(frag, GL_COMPILE_STATUS, &frag_Compiled);
	if (frag_Compiled != GL_TRUE) {
		GLsizei log_Length = 0;
		GLchar message[1024];
		glGetShaderInfoLog(frag, 1024, &log_Length, message);
		cout << message << endl;
	}

	// Attach the shaders
	glAttachShader(newShad.program, vert);
	glAttachShader(newShad.program, frag);

	// Link the shaders
	glLinkProgram(newShad.program);

	// Error checking for shader program
	GLint program_Linked;
	glGetProgramiv(newShad.program, GL_LINK_STATUS, &program_Linked);
	if (program_Linked != GL_TRUE) {
		GLsizei log_Length = 0;
		GLchar message[1024];
		glGetProgramInfoLog(newShad.program, 1024, &log_Length, message);
		cout << message << endl;
	}

	// Delete the shaders
	glDeleteShader(vert);
	glDeleteShader(frag);

	// Return the shader object
	return newShad;
}

void freeShader(shader &shad)
{
	glDeleteProgram(shad.program);
	shad = {};
}

void draw(const shader &shad, const geometry &geo)
{
	// Bind the shader program
	glUseProgram(shad.program);

	// Bind the VAO (geo and indices)
	glBindVertexArray(geo.vao);

	// Draw
	glDrawElements(GL_TRIANGLES, geo.size, GL_UNSIGNED_INT, 0);
}

void setUniform(const shader & shad, GLuint location, const glm::vec3 & value)
{
	glProgramUniform3fv(shad.program, location, 1, glm::value_ptr(value));
}

void errorCallback(GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, const GLchar *message, const void *userParam)
{
	std::cerr << message << std::endl;
}

void setUniform(const shader &shad, GLuint location, const glm::mat4 & value)
{
	glProgramUniformMatrix4fv(shad.program, location, 1, GL_FALSE, glm::value_ptr(value));
}

void setUniform(const shader & shad, GLuint location, const texture & value, int textureSlot)
{
	// Specify the texture slot we're working with
	glActiveTexture(GL_TEXTURE0 + textureSlot);

	// Bind the texture to that slot
	glBindTexture(GL_TEXTURE_2D, value.handle);

	// Assign the uniform to the shader
	glProgramUniform1i(shad.program, location, textureSlot);
}

texture makeTexture(unsigned width, unsigned height, unsigned channels, const unsigned char * pixels)
{
	GLenum oglFormat = GL_RGBA;
	switch (channels)
	{
	case 1:
		oglFormat = GL_RED;	
		break;
	case 2:
		oglFormat = GL_RG;
		break;
	case 3:
		oglFormat = GL_RGB;
		break;
	case 4:
		oglFormat = GL_RGBA;
		break;
	default:
		// Fatal error, halt the program
		context haltProgram;
		haltProgram.term();
		break;
	}

	texture tex = { 0, width, height, channels };

	// Generating and binding the texture
	glGenTextures(1, &tex.handle);
	glBindTexture(GL_TEXTURE_2D, tex.handle);
	
	// Buffer/send the actual data
	glTexImage2D(GL_TEXTURE_2D, 0, oglFormat, width, height, 0, oglFormat, GL_UNSIGNED_BYTE, pixels);

	// Describe how the texture will be used
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	// Unbind
	glBindTexture(GL_TEXTURE_2D, 0);

	return tex;
}

void freeTexture(texture & tex)
{
	glDeleteTextures(1, &tex.handle);
	tex = {};
}

texture loadTexture(const char * imagePath)
{
	int imageWidth, imageHeight, imageFormat;
	unsigned char *rawPixelData = nullptr;

	// Tell stb image to load the image
	stbi_set_flip_vertically_on_load(true);
	rawPixelData = stbi_load(imagePath, &imageWidth, &imageHeight, &imageFormat, STBI_default);

	// TODO: Ensure that rawPixelData is NOT NULL. If it is, the image failed to load

	// Pass the data to make texture to make the texture
	texture tex = makeTexture(imageWidth, imageHeight, imageFormat, rawPixelData);

	// Free the image
	stbi_image_free(rawPixelData);

	return tex;
}

//void retrieveStrings(const char * file, const char * wordToLookFor, int length)
//{
//	ifstream myFile;
//	string currentString;
//	string getlineString;
//	size_t foundWord = getlineString.find(wordToLookFor);
//
//	myFile.open(file, ios::in);
//	for (int SJ = 0; SJ < length; SJ++)
//	{
//		if (myFile.is_open)
//		{
//			while (getline(myFile, currentString))
//			{
//				if (foundWord != string::npos) {
//					getlineString += currentString;
//				}
//			}
//			myFile.close();
//		}
//	}
//}