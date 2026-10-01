using System;
using System.Collections.Generic;
using CalamityMod.Effects;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AerialTrackerLaser : ModProjectile, ILocalizedModType, IModType
{
	public Projectile disk;

	public Vector2 laserStart;

	public Vector2 laserEnd;

	public int lifetime = 15;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Time
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public float fade => (float)Math.Pow(Utils.GetLerpValue(0f, lifetime, base.Projectile.timeLeft), 3.0);

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.ArmorPenetration = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (Time == 0f)
		{
			disk = Main.projectile[(int)base.Projectile.ai[1]];
			targeted = Main.npc[(int)base.Projectile.ai[2]];
			if (disk != null)
			{
				laserStart = disk.Center;
			}
			else
			{
				laserStart = base.Projectile.Center;
			}
			if (targeted != null)
			{
				laserEnd = targeted.Center;
			}
			else
			{
				laserEnd = base.Projectile.Center;
			}
		}
		if (targeted.active && targeted.life > 0)
		{
			laserEnd = targeted.Center;
			base.Projectile.Center = targeted.Center;
		}
		Time++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.numHits <= 0)
		{
			return null;
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		CalamityGlobalNPC modNPC = target.Calamity();
		if (!modNPC.laserBurnMarked)
		{
			modNPC.laserBurnMarked = true;
			modNPC.laserBurnType = 1;
			modNPC.laserBurnTimer = 300;
		}
		modNPC.laserBurnTimer -= modNPC.laserBurnStacks * 2;
		modNPC.laserBurnDamage += (int)((float)base.Projectile.damage * 0.2f);
		modNPC.laserBurnStacks++;
		modifiers.SourceDamage *= 0f;
		modifiers.FinalDamage.Flat = 0.1f;
		modifiers.HideCombatText();
	}

	public override void OnKill(int timeLeft)
	{
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		if (disk != null && disk.active && disk.type == ModContent.ProjectileType<AerialTrackerProjectile>())
		{
			laserStart = disk.Center;
		}
		else
		{
			disk = null;
		}
		if (laserStart == Vector2.Zero || laserEnd == Vector2.Zero)
		{
			return false;
		}
		_ = Main.player[base.Projectile.owner];
		_ = TextureAssets.Projectile[base.Projectile.type].Value;
		Texture2D lineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineSoftEdge", (AssetRequestMode)2).Value;
		float distance = laserStart.Distance(laserEnd);
		int drawSeperation = 10;
		Vector2 toPoint = laserStart.DirectionTo(laserEnd);
		Color val;
		for (int i = drawSeperation; (float)i < distance - (float)drawSeperation; i += drawSeperation)
		{
			float completion = MathHelper.Lerp(0.8f, 3f, 1f - (float)i / distance);
			for (int y = 0; y < 2; y++)
			{
				Vector2 position = laserStart - Main.screenPosition + toPoint * (float)i;
				val = ((y == 1) ? Color.White : ArsenalEffects.ArsenalLaserColor);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(lineTex, position, null, val * fade, toPoint.ToRotation() + (float)Math.PI / 2f, lineTex.Size() * 0.5f, new Vector2(((y == 1) ? 0.3f : 1f) * MathHelper.Max(fade, 0.3f) * completion, 1.3f) * base.Projectile.scale * 0.01f, (SpriteEffects)0);
			}
		}
		Texture2D glowTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 position2 = laserStart - Main.screenPosition;
		val = ArsenalEffects.ArsenalLaserColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(glowTex, position2, null, val, base.Projectile.rotation, glowTex.Size() * 0.5f, 0.3f * fade, (SpriteEffects)0);
		Vector2 position3 = laserStart - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(glowTex, position3, null, val * 0.65f, base.Projectile.rotation, glowTex.Size() * 0.5f, 0.15f * fade, (SpriteEffects)0);
		return false;
	}
}
