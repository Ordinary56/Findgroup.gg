import asyncio
import psutil
import aiohttp
import sys
import json
BASE_URL = "http://localhost:5110/api"
userId = sys.argv[1]

def registerUrlScheme() -> None:
    pass

def list_processes() -> list[str]:
    '''
    Description
    -----------
    Lists all current running processes

    Returns 
    -------
    a list of process names
    '''
    processes: list[str] = []
    for pid in psutil.pids():
        try:
            process = psutil.Process(pid)
            processes.append(process.name())
        # If the process is protected or doesn't exists
        except (psutil.NoSuchProcess, psutil.AccessDenied):
            continue
    return processes

async def send_processes_to_backend(processes: list[str]):
    # Create session
    async with aiohttp.ClientSession() as session:
        try:
            async with session.post(BASE_URL + "/Activity/" + userId , json=json.dumps(processes)) as resp:
                if resp.status == 200:
                    # Send data
                    print("Successfully sent process list to the backend.")
                else:
                    # Handle non-successful status codes
                    print(f"Failed to send data. Status code: {resp.status}")
        except aiohttp.ClientError as e:
            print(f"Error occurred while sending data to backend: {e}")

async def main():
    # Get all processes
    list_of_processes: list[str] = list_processes()

    # Send list of processes to backend
    await send_processes_to_backend(list_of_processes)

if __name__ == "__main__":
    loop = asyncio.get_event_loop()
    loop.run_until_complete(main())
