using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod.Items.Accessories;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public sealed class PierceResistNPC : GlobalNPC
{
	internal static HashSet<int> exemptProjectiles;

	internal static HashSet<int> pierceResistNPC;

	internal static HashSet<int> singleHitboxNPC;

	internal static Dictionary<int, bool> singleHitboxExemptProjectiles;

	public override void Load()
	{
		exemptProjectiles = new HashSet<int>();
		pierceResistNPC = new HashSet<int>();
		singleHitboxNPC = new HashSet<int>();
		singleHitboxExemptProjectiles = new Dictionary<int, bool>();
	}

	public override void Unload()
	{
		exemptProjectiles?.Clear();
		exemptProjectiles = null;
		pierceResistNPC?.Clear();
		pierceResistNPC = null;
		singleHitboxNPC?.Clear();
		singleHitboxNPC = null;
		singleHitboxExemptProjectiles?.Clear();
		singleHitboxExemptProjectiles = null;
	}

	public override void SetStaticDefaults()
	{
		pierceResistNPC.Add(13);
		pierceResistNPC.Add(14);
		pierceResistNPC.Add(15);
		pierceResistNPC.Add(267);
		pierceResistNPC.Add(134);
		pierceResistNPC.Add(135);
		pierceResistNPC.Add(136);
		exemptProjectiles.Add(595);
		exemptProjectiles.Add(461);
		exemptProjectiles.Add(482);
		exemptProjectiles.Add(933);
		exemptProjectiles.Add(491);
		exemptProjectiles.Add(879);
		exemptProjectiles.Add(877);
		exemptProjectiles.Add(632);
		exemptProjectiles.Add(ModContent.ProjectileType<MarniteRepulsionHitbox>());
		exemptProjectiles.Add(707);
		exemptProjectiles.Add(927);
		exemptProjectiles.Add(878);
		exemptProjectiles.Add(735);
		singleHitboxExemptProjectiles[152] = true;
		singleHitboxExemptProjectiles[151] = true;
		singleHitboxExemptProjectiles[150] = true;
		singleHitboxExemptProjectiles[950] = true;
		singleHitboxExemptProjectiles[511] = true;
		singleHitboxExemptProjectiles[512] = true;
		singleHitboxExemptProjectiles[513] = true;
		foreach (ModProjectile projectile in ModContent.GetContent<ModProjectile>())
		{
			try
			{
				PierceResistExceptionAttribute pierceResistException = projectile.GetType().GetCustomAttribute<PierceResistExceptionAttribute>();
				if (pierceResistException != null)
				{
					int projectileType = projectile.Type;
					if (pierceResistException.OnlyForSingleHitbox)
					{
						singleHitboxExemptProjectiles[projectileType] = true;
					}
					else
					{
						exemptProjectiles.Add(projectileType);
					}
				}
			}
			catch (Exception value)
			{
				CalamityMod.Log.Error((object)$"Exception thrown while evaluating type \"{projectile.FullName}\": {value}");
			}
		}
		foreach (ModNPC npc in ModContent.GetContent<ModNPC>())
		{
			try
			{
				HasPierceResistAttribute hasPierceResist = npc.GetType().GetCustomAttribute<HasPierceResistAttribute>();
				if (hasPierceResist != null)
				{
					int npcType = npc.Type;
					pierceResistNPC.Add(npcType);
					if (hasPierceResist.SingleHitbox)
					{
						singleHitboxNPC.Add(npcType);
					}
				}
			}
			catch (Exception value2)
			{
				CalamityMod.Log.Error((object)$"Exception thrown while evaluating type \"{npc.FullName}\": {value2}");
			}
		}
	}

	public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (pierceResistNPC.Contains(npc.type) && !exemptProjectiles.Contains(projectile.type) && (!(singleHitboxExemptProjectiles.TryGetValue(projectile.type, out var isSingleHitboxExempt) & isSingleHitboxExempt) || !singleHitboxNPC.Contains(npc.type)))
		{
			PierceResistGlobal(projectile, npc, ref modifiers);
		}
	}

	private void PierceResistGlobal(Projectile projectile, NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (!CalamityNPCTypeSets.Thanatos.Contains(npc.type) || !npc.GetGlobalNPC<CalamityGlobalNPC>().unbreakableDR)
		{
			float damageReduction = (float)projectile.Calamity().timesPierced * 0.12f;
			if (damageReduction > 0.8f)
			{
				damageReduction = 0.8f;
			}
			modifiers.FinalDamage *= 1f - damageReduction;
			bool aiStyleExempt = projectile.aiStyle == 15 || projectile.aiStyle == 39 || projectile.aiStyle == 99;
			if ((projectile.penetrate > 1 || projectile.penetrate == -1) && !projectile.CountsAsClass<SummonDamageClass>() && !aiStyleExempt)
			{
				projectile.Calamity().timesPierced++;
			}
		}
	}
}
