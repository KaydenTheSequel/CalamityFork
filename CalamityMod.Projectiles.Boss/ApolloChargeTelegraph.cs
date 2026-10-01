using System.IO;
using CalamityMod.Events;
using CalamityMod.Graphics.Primitives;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ApolloChargeTelegraph : ModProjectile, ILocalizedModType, IModType
{
	public Vector2[] ChargePositions = (Vector2[])(object)new Vector2[1];

	public const float TelegraphFadeTime = 15f;

	public const float TelegraphWidth = 1132.0778f;

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
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		base.Projectile.timeLeft = (death ? 60 : (revenge ? 68 : (expertMode ? 75 : 90)));
		if (Main.getGoodWorld)
		{
			base.Projectile.timeLeft /= 2;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(ChargePositions.Length);
		for (int i = 0; i < ChargePositions.Length; i++)
		{
			writer.WriteVector2(ChargePositions[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		ChargePositions = (Vector2[])(object)new Vector2[reader.ReadInt32()];
		for (int i = 0; i < ChargePositions.Length; i++)
		{
			ChargePositions[i] = reader.ReadVector2();
		}
	}

	public override void AI()
	{
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
		bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float telegraphTotalTime = (num ? 60f : (revenge ? 68f : (expertMode ? 75f : 90f)));
		if (Main.getGoodWorld)
		{
			telegraphTotalTime *= 0.5f;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 6f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(telegraphTotalTime, telegraphTotalTime - 6f, base.Projectile.timeLeft, clamped: true);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public Color TelegraphPrimitiveColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		float opacity = MathHelper.Lerp(0.38f, 1.2f, base.Projectile.Opacity);
		opacity *= CalamityUtils.Convert01To010(completionRatio);
		opacity *= MathHelper.Lerp(0.9f, 0.2f, base.Projectile.ai[0] / ((float)ChargePositions.Length - 1f));
		if (completionRatio > 0.95f)
		{
			opacity = 1E-07f;
		}
		return Color.Green * opacity;
	}

	public float TelegraphPrimitiveWidth(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.Opacity * 15f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:Flame"].UseSaturation(0.41f);
		for (int i = ChargePositions.Length - 2; i >= 0; i--)
		{
			Vector2[] positions = (Vector2[])(object)new Vector2[5];
			for (int p = 0; p < positions.Length; p++)
			{
				positions[p] = Vector2.Lerp(ChargePositions[i], ChargePositions[i + 1], (float)p / ((float)positions.Length - 1f));
			}
			base.Projectile.ai[0] = i;
			PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(TelegraphPrimitiveWidth, TelegraphPrimitiveColor, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 55);
		}
		return false;
	}
}
