import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export interface UpdateSavingsPotDto {
  success: boolean
  reason: string
  amount: number
}

export default async function handler (req: NextApiRequest, res: NextApiResponse<UpdateSavingsPotDto>): Promise<void> {
  if (req.method !== '') {
    res.status(405)
  }

  const fetchResult = await backendFetchHelper.doPost('/Pots/UpdateSavingsPot', req.body)

  if (fetchResult.errored) {
    res.status(500).json({ success: false, reason: 'Unexpected error', amount: 0 })
  } else {
    res.status(200).json(fetchResult.data as UpdateSavingsPotDto)
  }
}
