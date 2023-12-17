import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export interface AddNewPotResponse {
  success: boolean
  reason: string
  potId?: number
}

export default async function handler (req: NextApiRequest, res: NextApiResponse<AddNewPotResponse>): Promise<void> {
  if (req.method !== 'POST') {
    res.status(405)
    return
  }

  const fetchResult = await backendFetchHelper.doPost('/Pots/AddNewPot', req.body)

  if (fetchResult.errored) {
    res.status(500)
  } else {
    res.status(200).json(fetchResult.data as AddNewPotResponse)
  }
}
