using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class BloodBomb : ModNPC, ILocalizedModType, IModType
{
	internal static Asset<Texture2D> BloodBombYellow;

	internal static Texture2D BloodBombGlow;

	public new string LocalizationCategory => "NPCs";

	public override void SetStaticDefaults()
	{
		NPCID.Sets.ProjectileNPC[base.Type] = true;
		BloodBombYellow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/BrainOfCthulhu/BloodBomb2", (AssetRequestMode)2);
	}

	internal static Texture2D GetBombGlow()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (BloodBombGlow == null)
		{
			Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, BloodBombYellow.Value.Width, BloodBombYellow.Value.Height);
			Color[] BaseArray = (Color[])(object)new Color[tex.Width * tex.Height];
			Color[] ColorArray = (Color[])(object)new Color[tex.Width * tex.Height];
			BloodBombYellow.Value.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			tex.SetData<Color>(ColorArray);
			BloodBombGlow = tex;
		}
		return BloodBombGlow;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.noGravity = true;
		base.NPC.damage = 10;
		base.NPC.defense = 5;
		base.NPC.lifeMax = 60;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = 0f;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCHit1 with
		{
			Pitch = 0.5f
		};
		base.NPC.knockBackResist = 0f;
		base.NPC.chaseable = false;
		base.NPC.noTileCollide = true;
		base.NPC.ShowNameOnHover = false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		if (source is EntitySource_Parent { Entity: NPC n })
		{
			base.NPC.ai[1] = n.whoAmI;
		}
		else
		{
			base.NPC.ai[1] = -1f;
		}
		Vector2 goal = Main.player[0].Center - base.NPC.Center;
		float vyi = 6f;
		float g = 0.2f;
		float t = Math.Abs((0f - MathF.Sqrt(36f + 2f * g * goal.Y) - vyi) / g);
		float vxi = goal.X / t;
		base.NPC.velocity = new Vector2(float.IsNaN(vxi) ? ((float)((goal.X > 0f) ? 10 : (-10))) : vxi, 0f - vyi);
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center, base.NPC.velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(2f, 3f), 32, 1f, Color.Red));
		}
		GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.NPC.Center, base.NPC.velocity * 2.5f, 16, 0.5f, Color.Red));
	}

	public override void AI()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[0] > 1200f)
		{
			base.NPC.active = false;
		}
		base.NPC.velocity.Y += 0.2f;
		base.NPC.rotation += (float)Math.Sign(base.NPC.velocity.X) * ((Vector2)(ref base.NPC.velocity)).Length() * 0.01f;
		if (base.NPC.ai[0] % 3f == 0f)
		{
			float deathRatio = 1 - base.NPC.life / base.NPC.lifeMax;
			float lerp = MathF.Sin(Main.GlobalTimeWrappedHourly * (10f + 10f * deathRatio)) / 2f + 0.5f;
			Color color = Color.Lerp(Color.Red, Color.Yellow, lerp);
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.NPC.Center + Main.rand.NextVector2Circular(base.NPC.width, base.NPC.height), (-base.NPC.velocity).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.25f, 0.75f), Main.rand.Next(10, 17), Main.rand.NextFloat(0.5f, 1f), color));
		}
		base.NPC.ai[0]++;
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (modifiers.DamageType == TrueMeleeDamageClass.Instance)
		{
			modifiers.FinalDamage *= 1.5f;
			return;
		}
		float dist = player.DistanceSQ(base.NPC.Center);
		dist = MathHelper.Clamp(dist - 10000f, 0f, 160000f);
		if (dist > 160000f)
		{
			modifiers.FinalDamage *= 0f;
		}
		else
		{
			modifiers.FinalDamage *= MathHelper.Lerp(1f, 0f, dist / 160000f);
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (modifiers.DamageType == TrueMeleeDamageClass.Instance)
		{
			modifiers.FinalDamage *= 1.5f;
			return;
		}
		if (projectile.owner == -1)
		{
			modifiers.FinalDamage *= 0f;
			return;
		}
		float dist = Main.player[projectile.owner].DistanceSQ(base.NPC.Center);
		dist = MathHelper.Clamp(dist - 10000f, 0f, 160000f);
		if (dist > 160000f)
		{
			modifiers.FinalDamage *= 0f;
		}
		else
		{
			modifiers.FinalDamage *= MathHelper.Lerp(1f, 0f, dist / 160000f);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			if (base.NPC.ai[1] == -1f)
			{
				base.NPC.ai[1] = base.NPC.Center.ClosestNPCAt(1200f, ignoreTiles: true, bossPriority: true).whoAmI;
			}
			Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, base.NPC.DirectionTo(Main.npc[(int)base.NPC.ai[1]].Center) * 16f, ModContent.ProjectileType<BloodBombRTS>(), 40, 1f, -1, base.NPC.ai[1]);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		float deathRatio = 1 - base.NPC.life / base.NPC.lifeMax;
		float time = Main.GlobalTimeWrappedHourly * (10f + 10f * deathRatio);
		float lerp = MathF.Sin(time) / 2f + 0.5f;
		Texture2D baseTex = TextureAssets.Npc[base.Type].Value;
		Texture2D overlayTex = BloodBombYellow.Value;
		Vector2 drawPos = base.NPC.Center - Main.screenPosition + Main.rand.NextVector2CircularEdge(2f * deathRatio, 2f * deathRatio);
		float scale = 1.5f + MathF.Sin(time) * 0.25f;
		if (Main.LocalPlayer.HasBuff(17))
		{
			spriteBatch.Draw(GetBombGlow(), drawPos, (Rectangle?)null, Color.OrangeRed, base.NPC.rotation, BloodBombGlow.Size() * 0.5f, scale + 0.15f, (SpriteEffects)0, 0f);
		}
		spriteBatch.Draw(baseTex, drawPos, (Rectangle?)null, Color.White, base.NPC.rotation, baseTex.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(overlayTex, drawPos, (Rectangle?)null, Color.White * lerp, base.NPC.rotation, overlayTex.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
