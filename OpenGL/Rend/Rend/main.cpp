#include "context.h"
#include "render.h"

#include <iostream>
#include <fstream>

#include "glew/GL/glew.h"
#include "glm/glm.hpp"
#include "glm/ext.hpp"

int main() 
{
	char * userInput;

	context game;
	game.init(640, 480, "SJ");

	// Triangle, CCW
	vertex triVerts[] = 
	{
		{ { -.5f, -.5f, 0, 1 },  { 0, 0, 1, 0 }, { 0.f, 0.f } },
		{ {  .5f,  -.5f, 0, 1 }, { 0, 0, 1, 0 }, {1.f, 0.f} },
		{ {    0,  .5f, 0, 1 },  { 0, 0, 1, 0 }, {.5f, 1.f} }
	};

	/*triVerts[0].vertexColor = glm::vec4(1.0, 0.0, 0.0, 1.0);
	triVerts[1].vertexColor = glm::vec4(4.0, 5.0, 6.0, 1.0);
	triVerts[2].vertexColor = glm::vec4(7.0, 8.0, 9.0, 1.0);*/

	unsigned int triIndices[] = { 0, 1, 2 };

	geometry triangle = makeGeometry(triVerts, 3, triIndices, 3);

	/*const char * basicVert =
		"#version 430\n"
		"layout(location = 0) in vec4 position;\n"
		"layout (location = 1) in vec4 normal;\n"
		"layout(location = 2) in vec2 uv;\n"
		"layout(location = 0) uniform mat4 proj;\n"
		"layout(location = 1) uniform mat4 view;\n"
		"layout(location = 2) uniform mat4 model;\n"
		"out vec2 vUV;\n"
		"out vec3 vNormal;\n"
		"void main() { gl_Position = proj * view * model * position; vUV = uv; vNormal = normalize(model * model).xyz; };";
	
	const char * basicFrag =
		"#version 430\n"
		"layout (location = 3) uniform sampler2D albedo;\n"
		"layout (location = 4) uniform vec3 lightDir;\n"
		"in vec2 vUV;\n"
		"in vec3 vNormal;\n"
		"out vec4 vertColor;\n"
		"void main() { float d = max(0, dot(vNormal, -lightDir)); vec4 diffuse = d * vec4(1,1,1,1); vec4 base = texture(albedo, vUV); vertColor = diffuse * base; }";*/

	// shader basicShad = makeShader(basicVert, basicFrag);
	shader basicShad = makeShader(loadShader("shaders/basic.vert.txt").c_str(), loadShader("shaders/basic.frag.txt").c_str());

	glm::mat4 triModel = glm::identity<glm::mat4>();

	glm::mat4 camProj = glm::perspective(glm::radians(45.f), 640.f / 480.f, 0.1f, 100.0f);

	glm::mat4 camView = glm::lookAt(glm::vec3(0, 0, -3), glm::vec3(0, 0, 0), glm::vec3(0, 1, 0));

	texture triTex = loadTexture("soulspear_diffuse.tga");

	light sun;
	sun.direction = glm::vec4{ -1, 0, 0, 1 };

	setUniform(basicShad, 0, camProj);
	setUniform(basicShad, 1, camView);
	setUniform(basicShad, 3, triTex, 0);
	setUniform(basicShad, 4, sun.direction);

	while (!game.shouldClose())
	{
		game.tick();
		game.clear();

		triModel = glm::rotate(triModel, glm::radians(5.f), glm::vec3(0, 1, 0));

		setUniform(basicShad, 2, triModel);

		draw(basicShad, triangle);
	}         
	game.term();

	return 0;
}