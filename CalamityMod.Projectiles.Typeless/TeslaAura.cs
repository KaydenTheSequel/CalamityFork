using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AcidRain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class TeslaAura : ModProjectile, ILocalizedModType, IModType
{
	private const float radius = 98f;

	private const int framesX = 3;

	private const int framesY = 6;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 218);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 18000;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft *= 5;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override void AI()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.localAI[0]++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.localAI[0] >= 6f)
		{
			base.Projectile.localAI[0] = 0f;
			base.Projectile.localAI[1]++;
		}
		if (base.Projectile.localAI[1] >= 3f)
		{
			base.Projectile.localAI[1] = 0f;
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.15f / 255f, (float)(255 - base.Projectile.alpha) * 0.15f / 255f, (float)(255 - base.Projectile.alpha) * 0.01f / 255f);
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.Center = player.Center;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 90);
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 6);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 90);
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 6);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Color drawColour = Color.White;
		Rectangle sourceRect = default(Rectangle);
		((Rectangle)(ref sourceRect))._002Ector(base.Projectile.width * (int)base.Projectile.localAI[1], base.Projectile.height * (int)base.Projectile.localAI[0], base.Projectile.width, base.Projectile.height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(base.Projectile.width / 2), (float)(base.Projectile.height / 2));
		Main.EntitySpriteDraw(color: drawColour * (Main.player[base.Projectile.owner].Calamity().teslaVisuals ? 1f : 0.25f), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: sourceRect, rotation: base.Projectile.rotation, origin: origin, scale: 1f, effects: (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 98f, targetHitbox);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (NPCID.Sets.ProjectileNPC[target.type] || (target.catchItem != 0 && target.type != ModContent.NPCType<Radiator>()))
		{
			return false;
		}
		return null;
	}
}
