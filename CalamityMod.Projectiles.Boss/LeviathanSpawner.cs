using System;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Boss;

public class LeviathanSpawner : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle RumbleSound = new SoundStyle("CalamityMod/Sounds/Custom/LeviathanRumble");

	public new string LocalizationCategory => "Projectiles.Boss";

	internal ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 450;
	}

	public override void AI()
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = CalamityUtils.Convert01To010((float)base.Projectile.timeLeft / 120f) * 4f;
		if (base.Projectile.Opacity > 1f)
		{
			base.Projectile.Opacity = 1f;
		}
		Main.LocalPlayer.Calamity().GeneralScreenShakePower = (float)Math.Pow(Utils.GetLerpValue(180f, 290f, Time, clamped: true), 0.3) * 6f;
		Main.LocalPlayer.Calamity().GeneralScreenShakePower += CalamityUtils.Convert01To010((float)Math.Pow(Utils.GetLerpValue(300f, 440f, Time, clamped: true), 0.5)) * 10f;
		if (base.Projectile.timeLeft % 180 == 0)
		{
			SoundEngine.PlaySound(in RumbleSound, base.Projectile.Center);
		}
		if (base.Projectile.timeLeft == 45)
		{
			if (!Main.dedServ)
			{
				WaterShaderData ripple = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();
				Vector2 ripplePos = base.Projectile.Center;
				for (int i = 0; i < 3; i++)
				{
					ripple.QueueRipple(ripplePos, Color.White, Vector2.One * 1000f, RippleShape.Square, Main.rand.NextFloat((float)Math.PI * 2f));
				}
			}
			if (Main.netMode == 1)
			{
				return;
			}
			int leviathan = NPC.NewNPC(base.Projectile.GetSource_FromThis(), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y, ModContent.NPCType<Leviathan>());
			if (Main.npc.IndexInRange(leviathan))
			{
				Main.npc[leviathan].velocity = Vector2.UnitY * -7f;
			}
		}
		CreateVisuals();
		Time++;
	}

	public void CreateVisuals()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		WorldUtils.Find((base.Projectile.Center - Vector2.UnitY * 1200f).ToTileCoordinates(), Searches.Chain(new Searches.Down(150), new CustomConditions.IsWater()), out var waterTop);
		if (Time % 4f == 3f && Time > 90f)
		{
			float xArea = MathHelper.Lerp(400f, 1150f, Time / 300f);
			Vector2 dustSpawnPosition = waterTop.ToWorldCoordinates() + Vector2.UnitY * 25f;
			dustSpawnPosition.X += Main.rand.NextFloatDirection() * xArea * 0.35f;
			Dust dust = Dust.NewDustPerfect(dustSpawnPosition, 267, Vector2.UnitY * -12f);
			dust.noGravity = true;
			dust.scale = 1.9f;
			dust.color = Color.CornflowerBlue;
			for (float x = 0f - xArea; x <= xArea; x += 110f)
			{
				float ripplePower = MathHelper.Lerp(4f, 10f, (float)Math.Sin(Main.GlobalTimeWrappedHourly + x / xArea * ((float)Math.PI * 2f)) * 0.5f + 0.5f);
				ripplePower *= MathHelper.Lerp(0.5f, 1f, Time / 300f);
				WaterShaderData obj = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();
				Vector2 ripplePos = waterTop.ToWorldCoordinates() + new Vector2(x, 32f) + Main.rand.NextVector2CircularEdge(50f, 50f);
				obj.QueueRipple(ripplePos, Color.White, Vector2.One * ripplePower, RippleShape.Square, Main.rand.NextFloat(-0.7f, 0.7f) + (float)Math.PI / 2f);
			}
		}
	}
}
