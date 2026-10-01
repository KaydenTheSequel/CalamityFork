using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.DataStructures;

public interface IDyeableShaderRenderer
{
	const float RoverDriveDepth = 1f;

	const float HaloShieldDepth = 2f;

	const float ProfanedSoulShieldDepth = 3f;

	const float SpongeShieldDepth = 4f;

	int OwnerPlayer { get; set; }

	float RenderDepth { get; }

	bool ShouldDrawDyeableShader { get; }

	bool ShaderIsDyeable => true;

	void DrawDyeableShader(SpriteBatch spriteBatch);
}
