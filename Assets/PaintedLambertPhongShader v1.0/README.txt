This Unity package contains a Unity material created with a Shader developed in ShaderGraph. This shader is a custom Lambert-Phong shader that allows an editor to change the colors of specular lighting and shadows. The editor can also change the brightness and size of the specular lighting.

Installation: Download and unzip the file. Open the Unity Editor, navigate to "Assets", and select "Import Package" -> "Custom Package". Once the file selector opens, navigate to and select the Unity Package file inside of the downloaded folder and select it. After selecting the package an import window will open, select "import" to include the shader material and source files into your project. To apply the imported material to an GameObject, simply drag the material onto it; otherwise, create a new material and change the material shader to "Painted Lambert Phong Shader", you can also drag this material onto any GameObject.

Alterable Shader Properties:
	Texture - Applies a texture onto the surface of this material.
	Tiling - The amount of times this texture is repeated on a surface on the x or y axis.
	Offset - Changes the position of the texture on the surface of this material by the specified amount on the x or y axis.
	ShadowColor - The color of this object on the side facing away from the primary light source.
	ShineBrightness - The brightness of the specular light reflected off this material.
	ShineColor - The color of the specular light reflected off this material.
	ShineSize - The size of area of specular light that covers the surface of this material.
	