import backendFetchHelper from '@/helpers/BackendFetchHelper'
import { type BoolReason } from '@/types/Shared/BoolReason'
import type { NextApiRequest, NextApiResponse } from 'next'

export default async function handler (req: NextApiRequest, res: NextApiResponse<BoolReason>): Promise<void> {
  if (req.method !== 'POST') {
    res.status(405)
  }

  const fetchResult = await backendFetchHelper.doPost('/AutomaticTransactions/UpdateAutomaticTransaction', req.body)

  if (fetchResult.errored) {
    res.status(500)
  } else {
    res.status(200).json(fetchResult.data as BoolReason)
  }
}
