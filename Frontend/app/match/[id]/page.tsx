
export default async function Match({params}: {params: Promise<{id: string}>}) {
    
    const { id } = await params;
    return (
        <div>
            <h1>Match id: {id}</h1>
        </div>
    );
}
