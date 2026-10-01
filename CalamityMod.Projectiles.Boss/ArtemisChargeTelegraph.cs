using System.IO;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ArtemisChargeTelegraph : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 OldVelocity;

	public const float TelegraphWidth = 2000f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public NPC ThingToAttachTo
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[1]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 45;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(OldVelocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		OldVelocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (ThingToAttachTo == null || !ThingToAttachTo.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = ThingToAttachTo.Center + base.Projectile.velocity * 50f;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 8f, base.Projectile.timeLeft, clamped: true);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public Color TelegraphPrimitiveColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		float increment = (completionRatio * 1.2f + Main.GlobalTimeWrappedHourly * 0.26f) % 1f;
		float opacity = MathHelper.Lerp(0.2f, 0.425f, base.Projectile.Opacity) * Utils.GetLerpValue(30f, 24f, base.Projectile.timeLeft, clamped: true);
		return CalamityUtils.MulticolorLerp(increment, Color.Orange, Color.Red, Color.Crimson, Color.Red) * opacity;
	}

	public float TelegraphPrimitiveWidth(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(1f, 0.995f, completionRatio, clamped: true) * base.Projectile.Opacity * 22f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:Flame"].UseSaturation(0.28f);
		Vector2[] drawPositions = (Vector2[])(object)new Vector2[5];
		for (int i = 0; i < drawPositions.Length; i++)
		{
			drawPositions[i] = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 2000f * (float)i / ((float)drawPositions.Length - 1f);
		}
		PrimitiveRenderer.RenderTrail(drawPositions, new PrimitiveSettings(TelegraphPrimitiveWidth, TelegraphPrimitiveColor, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 87);
		return false;
	}
}
