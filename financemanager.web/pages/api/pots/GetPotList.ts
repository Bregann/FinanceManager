import backendFetchHelper from '@/helpers/BackendFetchHelper'
import type { NextApiRequest, NextApiResponse } from 'next'

export interface PotList {
  potId: number
  potName: string
  potAmount: number
  isSavingsPot: boolean
}

export default async function handler (req: NextApiRequest, res: NextApiResponse<PotList>): Promise<void> {
  if (req.method !== 'GET') {
    res.status(405)
  }

  const fetchResult = await backendFetchHelper.doGet('/Pots/GetPotList')

  if (fetchResult.errored) {
    res.status(500)
  } else {
    res.status(200).json(fetchResult.data as PotList)
  }
}
